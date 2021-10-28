using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<string> _products;

        public BindingList<string> Products
        {
            get { return _products; }
            set 
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get { return _cart; }
            set {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }


        private string _itemQuantity;

        public string ItemQuantity
        {
            get { return _itemQuantity; }
            set {
                _itemQuantity = value;
                // notificar em produtos???
                NotifyOfPropertyChange(() => Products);
            }
        }

        public string SubTotal
        {
            get
            {
                //TO DO - Replace with calculation
                return "$0.00";
            }
        }

        public string Tax
        {
            get
            {
                //TO DO - Replace with calculation
                return "$0.00";
            }
        }

        public string Total
        {
            get
            {
                //TO DO - Replace with calculation
                return "$0.00";
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                //Make sure a product is selected
                //Make Sure theres is an item quantity

                return output;
            }
        }
        public void AddToCart()
        {

        }
        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                //Make sure a product is selected
                

                return output;
            }
        }
        public void RemoveFromCart()
        {

        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                //Make sure there's something in the cart


                return output;
            }
        }
        public void CheckOut()
        {

        }

    }
}
