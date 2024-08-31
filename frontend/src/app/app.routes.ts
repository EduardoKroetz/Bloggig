import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { FeedComponent } from './pages/feed/feed.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { UserComponent } from './pages/user/user.component';
import { SettingsComponent } from './pages/settings/settings.component';

export const routes: Routes = 
[
  { 
    path: "auth",
    component: AuthLayoutComponent,
    children: [
      { path: "login", component: LoginComponent },
      { path: "register", component: RegisterComponent }
    ]
  },
  { 
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', redirectTo: 'feed', pathMatch: 'full' },
      { path: 'feed', component: FeedComponent },
      { path: 'users/:id', component: UserComponent },
      { path: 'user/settings', component: SettingsComponent }
    ]
  },
  {
    path: '**', redirectTo: 'feed', pathMatch: "full"
  }
];
