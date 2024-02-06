import { Routes } from '@angular/router';
import { DashboardComponent } from './core/pages/dashboard/dashboard.component';
import { LoginComponent } from './core/pages/login/login.component';
import { RegisterComponent } from './core/pages/register/register.component';
import { PageNotFoundComponent } from './core/pages/page-not-found/page-not-found.component';
import { BookDetailsComponent } from './core/pages/book-details/book-details.component';
import { BikestoreComponent } from './core/pages/bikestore/bikestore.component';

export const routes: Routes = [
    { path: 'dashboard', component: DashboardComponent, title: "Dashboard Page" },
    { path: 'bikestore', component: BikestoreComponent, title: "Bike Store Page" },
    { path: 'book-details/:bookId', component: BookDetailsComponent, title: "Book Details Page" },
    { path: 'login', component: LoginComponent, title: "Login Page" },
    { path: 'register', component: RegisterComponent, title: "Register Page" },
    { path: '', redirectTo: '/bikestore', pathMatch: 'full' },
    { path: '**', component: PageNotFoundComponent, title: "Page Not Found" }
];
