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

  ngAfterViewInit(): void {
    // Kiểm tra xem phần tử DOM đã được khởi tạo chưa
    if (this.carousel && this.carousel.nativeElement) {
      // Khởi tạo Carousel thủ công bằng constructor của Bootstrap
      this.carouselInstance = new (window as any).bootstrap.Carousel(this.carousel.nativeElement, {
        interval: 5000, // Đặt thời gian chuyển slide (nếu cần)
        ride: 'carousel' // Khởi động tự động
      });
    } else {
      console.error('Carousel element not found!');
    }
  }

  touchStartSlider: Function = (e: any) => {
    this.startX = e.touches[0].clientX;
    console.log(this.startX);
  }

  touchMoveSlider: Function = (e: any) => {
    this.endX = e.touches[0].clientX;
    console.log(this.endX);
  }

  touchEnd: Function = (e: any) => {
    if (this.startX && this.endX && this.startX - this.endX > 50) {
      // Swipe left, go to next slide
      this.carouselInstance.next();
    } else if (this.startX && this.endX && this.endX - this.startX > 50) {
      // Swipe right, go to previous slide
      this.carouselInstance.prev();
    }

    console.log(`${this.startX} - ${this.endX}`);
    // Reset start and end positions
    this.startX = null;
    this.endX = null;
  }

  mouseStartSlider: Function = (e: MouseEvent) => {
    this.startX = e.clientX;
    console.log(this.startX);
  }

  mouseMoveSlider: Function = (e: MouseEvent) => {
    this.endX = e.clientX;
    console.log(this.endX);
  }

  mouseEnd: Function = (e: any) => {
    if (this.startX && this.endX && this.startX - this.endX > 50) {
      // Swipe left, go to next slide
      this.carouselInstance.next();
    } else if (this.startX && this.endX && this.endX - this.startX > 50) {
      // Swipe right, go to previous slide
      this.carouselInstance.prev();
    }

    console.log(`${this.startX} - ${this.endX}`);
    // Reset start and end positions
    this.startX = null;
    this.endX = null;
  }
}
