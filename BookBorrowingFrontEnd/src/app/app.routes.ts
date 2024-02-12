import { Routes } from '@angular/router';
import { DashboardComponent } from './core/pages/dashboard/dashboard.component';
import { LoginComponent } from './core/pages/login/login.component';
import { RegisterComponent } from './core/pages/register/register.component';
import { PageNotFoundComponent } from './core/pages/page-not-found/page-not-found.component';
import { BookDetailsComponent } from './core/pages/book-details/book-details.component';
import { BikestoreComponent } from './core/pages/bikestore/bikestore.component';
import { BikeDetailsComponent } from './core/pages/bike-details/bike-details.component';
import { AddBikeComponent } from './core/pages/add-bike/add-bike.component';
import { authGuard } from './core/guards/auth.guard';
import { adminGuard } from './core/guards/admin.guard';

export const routes: Routes = [
  {
    path: 'bikestore',
    component: BikestoreComponent,
    title: 'Bike Store Page',
  },
  {
    path: 'bike-details/:bikeId',
    component: BikeDetailsComponent,
    title: 'Bike Details Page',
  },
  {
    path: 'add-bike',
    component: AddBikeComponent,
    title: 'Add New Bike',
    canActivate: [authGuard, adminGuard],
  },
  { path: 'dashboard', component: DashboardComponent, title: 'Dashboard Page' },
  {
    path: 'book-details/:bookId',
    component: BookDetailsComponent,
    title: 'Book Details Page',
  },
  { path: 'login', component: LoginComponent, title: 'Login Page' },
  { path: 'register', component: RegisterComponent, title: 'Register Page' },
  { path: '', redirectTo: '/bikestore', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent, title: 'Page Not Found' },
];
