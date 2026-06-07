import { NgModule, Optional, SkipSelf } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from '../../services/api.service';
import { API_CONFIG, apiConfig } from '../../config/api.config';

@NgModule({
  imports: [HttpClientModule],
  providers: [
    ApiService,
    { provide: API_CONFIG, useValue: apiConfig }
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule | null) {
    if (parentModule) {
      throw new Error('CoreModule is already loaded. Import it in AppModule only.');
    }
  }
}
