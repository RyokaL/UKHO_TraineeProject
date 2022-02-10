import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { MsalModule } from '@azure/msal-angular';
import { PublicClientApplication } from '@azure/msal-browser'

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CharInfoComponent } from './char-info/char-info.component';
import { ParseLogComponent } from './parse-log/parse-log.component';
import { UserLoginOutNavComponent } from './user-login-out-nav/user-login-out-nav.component';

const isIE = window.navigator.userAgent.indexOf('MSIE ') > -1 || window.navigator.userAgent.indexOf('Trident/') > -1;

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CharInfoComponent,
    ParseLogComponent,
    UserLoginOutNavComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'characters', component: CharInfoComponent },
      { path: 'characters/:id/parses', component: ParseLogComponent },
    ]),

    MsalModule.forRoot( new PublicClientApplication({
      auth: {
        clientId: 'b77cbc4e-15f2-475b-8588-0163bcb9499a', // This is your client ID
        authority: 'b17b2ce2-1c2f-4882-b3a6-469de5140ef2', // This is your tenant ID
        redirectUri: 'http://localhost:44317'// This is your redirect URI
      },
      cache: {
        cacheLocation: 'localStorage',
        storeAuthStateInCookie: isIE, // Set to true for Internet Explorer 11
      }
    }), null, null),

  ],
  providers: [],
  bootstrap: [AppComponent]
})


export class AppModule { }