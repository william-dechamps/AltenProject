import {
    Component,
    inject,
    OnInit,
} from "@angular/core";
import { RouterModule } from "@angular/router";
import { CartService } from "./cart.service";
import { CardModule } from "primeng/card";
import { DataViewModule } from "primeng/dataview";
import { ButtonModule } from "primeng/button";
import { InputNumberModule } from "primeng/inputnumber";
import { FormsModule } from "@angular/forms";
import { CartItem } from "./data-access/cart.model";
import { AsyncPipe } from "@angular/common";

@Component({
    selector: "cart",
    templateUrl: "./cart.component.html",
    styleUrls: ["./cart.component.scss"],
    standalone: true,
    imports: [DataViewModule, RouterModule, CardModule, ButtonModule, InputNumberModule, AsyncPipe, FormsModule],
})
export class CartComponent implements OnInit {
    private readonly cartService = inject(CartService);
    public productsInCart = this.cartService.cart$;

    ngOnInit() {
        this.cartService.getCartItems().subscribe();
    }

    onQuantityChange(cartItem: CartItem, newQuantity: number) {
        this.cartService.updateCart(cartItem.product, newQuantity);
    }

    removeFromCart(cartItem: CartItem) {
        this.cartService.updateCart(cartItem.product, 0);
    }
}
