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

    slideConfig =
        {
            slidesToShow: 4,
            slidesToScroll: 4,
            dots: false,
            //prevArrow: "<button type='button' class='slick-prev slick-prev-custom'>previous</button>",
            //nextArrow: "<button type='button' class='slick-next'>next</button>",
        }


    list = [1, 2, 3, 4, 5, 6, 7, 8]

    listTypes = [
        { imageSrc: 'assets/images/iphone-16-pro-max-product.png', titleType: 'Iphone' },
        { imageSrc: 'assets/images/samsung-galaxy-s23-ultra-product.png', titleType: 'Samsung' },
        { imageSrc: 'assets/images/oppo-find-x6-pro-product.png', titleType: 'Oppo' },
        { imageSrc: 'assets/images/xiaomi-13-pro-product.png', titleType: 'Xiaomi' },
    ]

}
