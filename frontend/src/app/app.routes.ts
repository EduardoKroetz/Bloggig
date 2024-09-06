import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { FeedComponent } from './pages/feed/feed.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { UserComponent } from './pages/user/user.component';
import { SettingsComponent } from './pages/settings/settings.component';
import { NewPostComponent } from './pages/new-post/new-post.component';
import { EditPostComponent } from './pages/edit-post/edit-post.component';
import { SearchComponent } from './pages/search/search.component';
import { ProfileImageComponent } from './pages/profile-image/profile-image.component';

export const routes: Routes = 
[
  { 
    path: "auth",
    component: AuthLayoutComponent,
    children: [
      { path: "login", component: LoginComponent },
      { path: "register", component: RegisterComponent },
    ]
  },
  { 
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', redirectTo: 'feed', pathMatch: 'full' },
      { path: 'feed', component: FeedComponent },
      { path: 'users/:id', component: UserComponent },
      { path: 'user/settings', component: SettingsComponent },
      { path: 'posts/new-post', component: NewPostComponent },
      { path: 'posts/edit-post/:id', component: EditPostComponent },
      { path: 'search', component: SearchComponent },
      { path: "profile-image", component: ProfileImageComponent }
    ] 
  },
  {
    path: '**', redirectTo: 'feed', pathMatch: "full"
  }
];
