import { Component } from '@angular/core';

@Component({
  selector: 'app-drag-slider',
  templateUrl: './drag-slider.component.html',
  styleUrls: ['./drag-slider.component.scss']
})
export class DragSliderComponent {
  ngAfterViewInit(){
    const carousel = document.querySelector<HTMLElement>(".carousel") ?? new HTMLElement();
    const firstImg = carousel.querySelectorAll<HTMLElement>("img")[0];
    const arrowIcons = document.querySelectorAll<HTMLElement>(".wrapper i");
    let isDragStart = false, isDragging = false, prevPageX : any, prevScrollLeft : any, positionDiff: any;
      const showHideIcons = () => {
          // showing and hiding prev/next icon according to carousel scroll left value
          let scrollWidth = carousel.scrollWidth - carousel.clientWidth; // getting max scrollable width
          arrowIcons[0].style.display = carousel.scrollLeft == 0 ? "none" : "block";
          arrowIcons[1].style.display = carousel.scrollLeft == scrollWidth ? "none" : "block";
      }
      arrowIcons.forEach(icon => {
          icon.addEventListener("click", () => {
              let firstImgWidth = firstImg.clientWidth + 14; // getting first img width & adding 14 margin value
              // if clicked icon is left, reduce width value from the carousel scroll left else add to it
              carousel.scrollLeft += icon.id == "left" ? -firstImgWidth : firstImgWidth;
              setTimeout(() => showHideIcons(), 60); // calling showHideIcons after 60ms
          });
      });
      function autoSlide() {
      // if there is no image left to scroll then return from here
      if (carousel.scrollLeft - (carousel.scrollWidth - carousel.clientWidth) > -1 || carousel.scrollLeft <= 0) return;
      positionDiff = Math.abs(positionDiff); // making positionDiff value to positive
      let firstImgWidth = firstImg.clientWidth + 14;
      // getting difference value that needs to add or reduce from carousel left to take middle img center
      let valDifference = firstImgWidth - positionDiff;
      if (carousel.scrollLeft > prevScrollLeft) { // if user is scrolling to the right
        return carousel.scrollLeft += positionDiff > firstImgWidth / 3 ? valDifference : -positionDiff;
      }
      // if user is scrolling to the left
      carousel.scrollLeft -= positionDiff > firstImgWidth / 3 ? valDifference : -positionDiff;
      return null;
    }
      const dragStart = (e: any) => {
          // updatating global variables value on mouse down event
          console.log("dragStart");
          isDragStart = true;
          prevPageX = e.pageX || e.touches[0].pageX;
          prevScrollLeft = carousel.scrollLeft;
      }
      const dragging = (e: any) => {
          // scrolling images/carousel to left according to mouse pointer
          if(!isDragStart) return;
          e.preventDefault();
          console.log("dragging");
          isDragging = true;
          carousel.classList.add("dragging");
          positionDiff = (e.pageX || e.touches[0].pageX) - prevPageX;
          carousel.scrollLeft = prevScrollLeft - positionDiff;
          showHideIcons();
      }
      const dragStop = () => {
          console.log("dragStop");
          isDragStart = false;
          carousel.classList.remove("dragging");
          if(!isDragging) return;
          isDragging = false;
          autoSlide();
      }
      document.addEventListener("mousedown", dragStart);
      document.addEventListener("touchstart", dragStart);
      document.addEventListener("mousemove", dragging);
      document.addEventListener("touchmove", dragging);
      document.addEventListener("mouseup", dragStop);
      document.addEventListener("touchend", dragStop);
  }
}
