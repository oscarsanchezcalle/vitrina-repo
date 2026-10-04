import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthSession } from './core/interfaces/auth-session.interface';
import { SessionService } from './core/services/session.service';

@Component({
  imports: [RouterLink, RouterOutlet],
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  readonly title = 'Vitrina';

  private readonly router = inject(Router);
  protected readonly sessionService = inject(SessionService);

  get session(): AuthSession | null {
    return this.sessionService.getSession();
  }

  signOut(): void {
    this.sessionService.clear();
    void this.router.navigateByUrl('/auth/login');
  }
}
