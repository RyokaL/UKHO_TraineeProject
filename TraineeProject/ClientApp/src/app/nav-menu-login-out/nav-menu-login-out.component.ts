import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { MsalBroadcastService, MsalGuardConfiguration, MsalService, MSAL_GUARD_CONFIG, MsalRedirectComponent } from '@azure/msal-angular';
import { AuthenticationResult, InteractionStatus, InteractionType, PopupRequest, RedirectRequest } from '@azure/msal-browser';
import { Subject } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-nav-menu-login-out',
  templateUrl: './nav-menu-login-out.component.html',
  styleUrls: ['./nav-menu-login-out.component.css']
})
export class NavMenuLoginOutComponent implements OnInit, OnDestroy {

  loggedIn = false;
  isIframe = false;
  private readonly _destroying$ = new Subject<void>();

  constructor(
    @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
    private authService: MsalService,
    private msalBroadcastService: MsalBroadcastService
  ) { }

  ngOnInit(): void {
    this.isIframe = window !== window.parent && !window.opener;

    this.msalBroadcastService.inProgress$
      .pipe(
        filter((status: InteractionStatus) => status === InteractionStatus.None),
        takeUntil(this._destroying$)
      )
      .subscribe(() => {
        this.setLoggedIn();
      });
  }

  setLoggedIn(): void {
    this.loggedIn = this.authService.instance.getAllAccounts().length > 0;
  }

  login(): void {
    if (this.msalGuardConfig.authRequest){
      this.authService.loginRedirect({...this.msalGuardConfig.authRequest} as RedirectRequest);
    } else {
      this.authService.loginRedirect();
    }
  }

  logout(): void {
    this.authService.logoutRedirect({
      postLogoutRedirectUri: "/",
    });
  }

  ngOnDestroy(): void {
    this._destroying$.next(undefined);
    this._destroying$.complete();
  }

}