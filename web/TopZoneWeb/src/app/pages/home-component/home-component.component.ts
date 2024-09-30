import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-home-component',
  templateUrl: './home-component.component.html',
  styleUrls: ['./home-component.component.scss']
})
export class HomeComponentComponent {

  @ViewChild("carouselExampleInterval", { static: false }) carousel!: ElementRef;
  startX: number | null = null;
  endX: number | null = null;
  carouselInstance: any;

  slideConfig = { slidesToShow: 1,slidesToScroll: 1,dots: true}

  list = [1,2,3]

}
