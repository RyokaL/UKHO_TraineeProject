import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CharInfoComponent } from './char-info/char-info.component';
import { ParseLogComponent } from './parse-log/parse-log.component';
import { NavMenuLoginOutComponent } from './nav-menu-login-out/nav-menu-login-out.component';

import { IPublicClientApplication, PublicClientApplication, InteractionType, BrowserCacheLocation, LogLevel } from '@azure/msal-browser';
import { MsalGuard, MsalBroadcastService, MsalModule, MsalService, MSAL_GUARD_CONFIG, MSAL_INSTANCE, MsalGuardConfiguration, MsalRedirectComponent, MsalInterceptor, MsalInterceptorConfiguration, MSAL_INTERCEPTOR_CONFIG } from '@azure/msal-angular';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UploadLogFileComponent } from './upload-log-file/upload-log-file.component';

//Config options for Azure B2C, had to assume based on a few tutorials but seems to work
export const b2cPolicies = {
  names: {
      //Name of the user flow for sign up/sign in
      signUpSignIn: "B2C_1_TraineeProjectSignUpSignIn"
  },
  //These are the endpoints to redirect to for above user flows
  authorities: {
      signUpSignIn: {
          authority: "https://ukhofflogs.b2clogin.com/ukhofflogs.onmicrosoft.com/B2C_1_TraineeProjectSignUpSignIn",
      }
  },
  //The default authority domain - domain of the B2C tenant
  authorityDomain: "ukhofflogs.b2clogin.com"
};

export function loggerCallback(logLevel: LogLevel, message: string) {
  console.log(message);
}

//This is the way Azure tutorial had the config, assuming there's different types of config?
export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      //Azure B2C client id, is this a secret? Doesn't say so
      clientId: 'b77cbc4e-15f2-475b-8588-0163bcb9499a',
      authority: b2cPolicies.authorities.signUpSignIn.authority,
      knownAuthorities: [b2cPolicies.authorityDomain],
      redirectUri: '/', //This must be registered in Azure, it is where the user will be redirected back to
      postLogoutRedirectUri: '/',
      navigateToLoginRequestUrl: true
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage, //Apparently SessionStorage is more secure, but LocalStorage persists the session
      storeAuthStateInCookie: false, // Seems to be useful for bugs in IE11 but IE11 is unsupported so probably not an issue
    },
    system: {
      loggerOptions: {
        loggerCallback,
        logLevel: LogLevel.Info,
        piiLoggingEnabled: false
      }
    }
  });
}

//MSAL Guard allows routes to only be accessible to logged in users
export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return { 
    interactionType: InteractionType.Redirect
  };
}

export function MsalInterceptorConfigFactory(): MsalInterceptorConfiguration {
  return {
    interactionType: InteractionType.Popup,
    protectedResourceMap: new Map([["https://localhost:5001/api/character", ["https://ukhofflogs.onmicrosoft.com/294c86d0-147f-4d34-b6a5-013fe967b2f3/access_as_user"]]])
  };
}

@NgModule({
  //Declares what components are available to Angular
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CharInfoComponent,
    ParseLogComponent,
    NavMenuLoginOutComponent,
    UserProfileComponent,
    UploadLogFileComponent
  ],
  //Dependency injection section - provides instances to other components that ask for it
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MsalModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'characters', component: CharInfoComponent },
      { path: 'characters/:id/parses', component: ParseLogComponent },
      { path: 'profile', component: UserProfileComponent, canActivate: [MsalGuard] }
    ])
  ],
  //If a component asks for a data structure, provide the function that will give it
  providers: [
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MsalInterceptorConfigFactory
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
    MsalService,
    MsalGuard,
    MsalBroadcastService
  ],
  //MsalRedirectComponent needs to be bootstrapped here to handle redirect promises correctly.
  bootstrap: [AppComponent, MsalRedirectComponent]
})
export class AppModule { }
