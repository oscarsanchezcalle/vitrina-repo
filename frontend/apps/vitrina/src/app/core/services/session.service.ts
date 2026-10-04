import { Injectable, signal } from '@angular/core';
import { AuthSession } from '../interfaces/auth-session.interface';

@Injectable({ providedIn: 'root' })
export class SessionService {
  private readonly storageKey = 'vitrina.auth.session';
  private readonly sessionState = signal<AuthSession | null>(this.readStorage());

  readonly session = this.sessionState.asReadonly();

  getSession(): AuthSession | null {
    const session = this.sessionState();
    if (!session) {
      return null;
    }

    if (this.isExpired(session)) {
      this.clear();
      return null;
    }

    return session;
  }

  setSession(session: AuthSession): void {
    this.sessionState.set(session);
    this.writeStorage(session);
  }

  clear(): void {
    this.sessionState.set(null);

    if (!this.hasStorage()) {
      return;
    }

    localStorage.removeItem(this.storageKey);
  }

  isAuthenticated(): boolean {
    return this.getSession() !== null;
  }

  hasRole(role: string): boolean {
    return this.getSession()?.role === role;
  }

  private isExpired(session: AuthSession): boolean {
    const expiresAt = Date.parse(session.expiresAt);
    return Number.isFinite(expiresAt) && expiresAt <= Date.now();
  }

  private readStorage(): AuthSession | null {
    if (!this.hasStorage()) {
      return null;
    }

    try {
      const rawSession = localStorage.getItem(this.storageKey);
      if (!rawSession) {
        return null;
      }

      return JSON.parse(rawSession) as AuthSession;
    } catch {
      return null;
    }
  }

  private writeStorage(session: AuthSession): void {
    if (!this.hasStorage()) {
      return;
    }

    localStorage.setItem(this.storageKey, JSON.stringify(session));
  }

  private hasStorage(): boolean {
    return typeof localStorage !== 'undefined';
  }
}
