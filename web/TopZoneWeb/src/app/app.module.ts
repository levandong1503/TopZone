import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TypeCardComponent } from './components/type-card/type-card.component';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponentComponent } from './pages/home-component/home-component.component';
import { HomeModule } from './modules/home/home.module';
import { SharedModule } from './modules/shared/shared.module';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { CoreModule } from './modules/core/core.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponentComponent,
  ],
  imports: [
    BrowserModule,
    CoreModule,
    AppRoutingModule,
    HomeModule,
    SharedModule,
    SlickCarouselModule,
    TypeCardComponent
  ],
  providers: [],
  schemas: [NO_ERRORS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
