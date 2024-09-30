import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DragSliderComponent } from './drag-slider/drag-slider.component';



@NgModule({
  declarations: [
    DragSliderComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    DragSliderComponent
  ]
})
export class SharedModule { }
