import { HttpErrorResponse } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { provideRouter, Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { AuthApiService } from '../../core/services/auth-api.service';
import { SessionService } from '../../core/services/session.service';
import { LoginPageComponent } from './login-page.component';

describe('LoginPageComponent', () => {
  const authApiService = {
    login: jest.fn()
  };
  const sessionService = {
    setSession: jest.fn()
  };

  beforeEach(async () => {
    authApiService.login.mockReset();
    sessionService.setSession.mockReset();

    await TestBed.configureTestingModule({
      imports: [LoginPageComponent],
      providers: [
        provideRouter([]),
        {
          provide: AuthApiService,
          useValue: authApiService
        },
        {
          provide: SessionService,
          useValue: sessionService
        }
      ]
    }).compileComponents();
  });

  it('does not submit when the form is invalid', () => {
    const fixture = TestBed.createComponent(LoginPageComponent);
    fixture.componentInstance.submit();

    expect(authApiService.login).not.toHaveBeenCalled();
    expect(fixture.componentInstance.form.touched).toBe(true);
  });

  it('stores the session and navigates to products after a successful login', () => {
    const fixture = TestBed.createComponent(LoginPageComponent);
    const router = TestBed.inject(Router);
    const navigateByUrlSpy = jest
      .spyOn(router, 'navigateByUrl')
      .mockResolvedValue(true);

    authApiService.login.mockReturnValue(
      of({
        accessToken: 'token',
        expiresAt: '2026-10-05T00:00:00Z',
        name: 'Admin User',
        username: 'admin',
        role: 'Admin'
      })
    );

    fixture.componentInstance.form.setValue({
      username: 'admin',
      password: 'Admin123!'
    });

    fixture.componentInstance.submit();

    expect(authApiService.login).toHaveBeenCalledWith({
      username: 'admin',
      password: 'Admin123!'
    });
    expect(sessionService.setSession).toHaveBeenCalledWith({
      accessToken: 'token',
      expiresAt: '2026-10-05T00:00:00Z',
      name: 'Admin User',
      username: 'admin',
      role: 'Admin'
    });
    expect(navigateByUrlSpy).toHaveBeenCalledWith('/products');
  });

  it('preserves backend validation messages on authentication failure', () => {
    const fixture = TestBed.createComponent(LoginPageComponent);

    authApiService.login.mockReturnValue(
      throwError(
        () =>
          new HttpErrorResponse({
            status: 401,
            error: {
              succeeded: false,
              message: 'Authentication failed.',
              errors: ['Invalid username or password.']
            }
          })
      )
    );

    fixture.componentInstance.form.setValue({
      username: 'admin',
      password: 'wrong'
    });

    fixture.componentInstance.submit();

    expect(fixture.componentInstance.requestError()).toEqual({
      succeeded: false,
      message: 'Authentication failed.',
      errors: ['Invalid username or password.']
    });
  });
});
