import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
    selector: 'app-catalog',
    standalone: true,
    imports: [CommonModule, RouterModule],
    templateUrl: './catalog.component.html',
    styleUrls: ['./catalog.component.scss'],
})
export class CatalogComponent {}
