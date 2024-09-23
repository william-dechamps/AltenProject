import { Component, OnInit, inject } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CartService } from "app/cart/cart.service";
import { Product, InventoryStatus } from "app/products/data-access/product.model";
import { ProductsService } from "app/products/data-access/products.service";
import { ButtonModule } from "primeng/button";
import { CardModule } from "primeng/card";
import { DataViewModule } from 'primeng/dataview';
import { DialogModule } from 'primeng/dialog';
import { RatingModule } from 'primeng/rating';
import { TagModule } from 'primeng/tag';

@Component({
  selector: "app-product-list",
  templateUrl: "./product-list.component.html",
  styleUrls: ["./product-list.component.scss"],
  standalone: true,
  imports: [DataViewModule, CardModule, ButtonModule, RatingModule, DialogModule, FormsModule, TagModule],
})
export class ProductListComponent implements OnInit {
  private readonly productsService = inject(ProductsService);
  private readonly cartService = inject(CartService);
  public readonly InventoryStatus = InventoryStatus;
  public readonly products = this.productsService.products;

  ngOnInit() {
    this.productsService.get().subscribe();
  }

  addToCart(product: Product) {
    this.cartService.addToCart(product);
  }
}
