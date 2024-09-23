import {
    Component,
    inject,
} from "@angular/core";
import { RouterModule } from "@angular/router";
import { CartService } from "./cart.service";
import { CardModule } from "primeng/card";
import { DataViewModule } from "primeng/dataview";
import { ButtonModule } from "primeng/button";
import { CartItem } from "./data-access/cart.model";
import { BehaviorSubject } from "rxjs";

@Component({
    selector: "cart",
    templateUrl: "./cart.component.html",
    styleUrls: ["./cart.component.scss"],
    standalone: true,
    imports: [DataViewModule, RouterModule, CardModule, ButtonModule],
})
export class CartComponent {
    private readonly cartService = inject(CartService);
    public productsInCart = this.cartService.getCart().cartItems;

    ngOnInit() {
        this.cartService.cartItems$.subscribe(items => {
            this.productsInCart = items;
        });
    }

    removeFromCart(cartItem: CartItem) {
        this.cartService.updateCart(cartItem.product, -1);
    }
}
