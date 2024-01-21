import { Routes } from '@angular/router';
import { DashboardComponent } from './core/pages/dashboard/dashboard.component';
import { LoginComponent } from './core/pages/login/login.component';
import { RegisterComponent } from './core/pages/register/register.component';
import { PageNotFoundComponent } from './core/pages/page-not-found/page-not-found.component';

export const routes: Routes = [
    { path: 'dashboard', component: DashboardComponent, title: "Dashboard Page" },
    { path: 'login', component: LoginComponent, title: "Login Page" },
    { path: 'register', component: RegisterComponent, title: "Register Page" },
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: '**', component: PageNotFoundComponent, title: "Page Not Found" }
];
