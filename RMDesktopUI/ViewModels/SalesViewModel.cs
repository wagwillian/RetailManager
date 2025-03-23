using AutoMapper;
using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using RMDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private readonly IProductEndPoint _productEndPoint;
        private readonly IConfiguration _config;
        private readonly ISaleEndPoint _saleEndPoint;
        private readonly IMapper _mapper;
        private readonly StatusInfoViewModel _status;
        private readonly IWindowManager _window;

        public SalesViewModel(IProductEndPoint productEndPoint, IConfiguration config, ISaleEndPoint saleEndPoint,
            IMapper mapper, StatusInfoViewModel status, IWindowManager window)
        {
            _productEndPoint = productEndPoint;
            _config = config;
            _saleEndPoint = saleEndPoint;
            _mapper = mapper;
            _status = status;
            _window = window;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadProducts();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";


                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorized Acces", "You do not have permission to interact with the Sales Form.");
                    await _window.ShowDialogAsync(_status, null, settings); 
                }
                else
                {
                    _status.UpdateMessage("Fatal Exception", ex.Message);
                    await _window.ShowDialogAsync(_status, null, settings);
                }

                await TryCloseAsync();
                
            }
            
        }

        private async Task LoadProducts()
        {
            var productList = await _productEndPoint.GetAll();
            var products = _mapper.Map<List<ProductDisplayModel>>(productList);
            Products = new BindingList<ProductDisplayModel>(products);
        }

        private BindingList<ProductDisplayModel> _products;

        public BindingList<ProductDisplayModel> Products
        {
            get { return _products; }
            set 
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private ProductDisplayModel _selectedIProduct;

        public ProductDisplayModel SelectedIProduct
        {
            get { return _selectedIProduct; }
            set
            {
                _selectedIProduct = value;
                NotifyOfPropertyChange(() => SelectedIProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private async Task ResetSalesViewModel()
        {
            Cart = new BindingList<CartItemDisplayModel>();
            await LoadProducts();

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        private CartItemDisplayModel _selectedCartItem;

        public CartItemDisplayModel SelectedCartItem
        {
            get { return _selectedCartItem; }
            set
            {
                _selectedCartItem = value;
                NotifyOfPropertyChange(() => SelectedCartItem);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
            }
        }


        private BindingList<CartItemDisplayModel> _cart = new();

        public BindingList<CartItemDisplayModel> Cart
        {
            get { return _cart; }
            set 
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }


        private int _itemQuantity = 1;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public string SubTotal
        {
            get
            {                
                return CaculateSubTotal().ToString("C");
            }
        }

        private decimal CaculateSubTotal()
        {
            decimal subTotal = 0;
            foreach (var item in Cart)
            {
                subTotal += (item.Product.RetailPrice * item.QuantityInCart);
            }
            return subTotal;
        }
        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _config.GetValue<decimal>("TaxRagte") / 100;

           taxAmount = Cart
                .Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.QuantityInCart * taxRate);
            
            //foreach (var item in Cart)
            //{
            //    if (item.Product.IsTaxable)
            //    {
            //        taxAmount += (item.Product.RetailPrice * item.QuantityInCart * taxRate);
            //    }
            //}
            return taxAmount;
        }
        public string Tax
        {
            get
            {                
                return CalculateTax().ToString("C");
            }            
        }

        public string Total
        {
            get
            {
                decimal total = CaculateSubTotal() + CalculateTax();
                return total.ToString("C");
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                //Make sure a product is selected
                //Make Sure theres is an item quantity
                if (ItemQuantity > 0 && SelectedIProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }


                return output;
            }
        }
        public void AddToCart()
        {
            CartItemDisplayModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedIProduct);

            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;
                //Gambiarra para NotifyOfPropertyChange(() => Cart); funcionar e atualizar o a quantidade de items no carrinho (arrumado com automapper!!!)
                //Cart.Remove(existingItem);
                //Cart.Add(existingItem);
            }   
            else
            {
                CartItemDisplayModel item = new()
                {
                    Product = SelectedIProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(item);                
            }

            SelectedIProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => Cart);
            NotifyOfPropertyChange(() => CanCheckOut);

        }
        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                //Make sure a product is selected && adiciona um bug não poder remover item quando o stock esta vazio.
                if (SelectedCartItem != null&& SelectedCartItem?.QuantityInCart > 0)
                {
                    output = true;
                }
                

                return output;
            }
        }
        public void RemoveFromCart()
        {
            
            SelectedCartItem.Product.QuantityInStock += 1;
            if (SelectedCartItem.QuantityInCart > 1)
            {
                SelectedCartItem.QuantityInCart -= 1;
            }
            else
            {
                Cart.Remove(SelectedCartItem);
            }
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
            NotifyOfPropertyChange(() => CanAddToCart);
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                //Make sure there's something in the cart
                if (Cart.Count > 0)
                {
                    output = true;
                }


                return output;
            }
        }
        public async Task CheckOut()
        {
            //Create a SaleModel and post to API
            SaleModel sale = new();           


            foreach (var item in Cart)
            {
                sale.SaleDetails.Add(new SaleDetailModel()
                {
                    ProductId = item.Product.Id,
                    Quantity = item.QuantityInCart,
                });                         

            }
            
            await _saleEndPoint.PostSale(sale);

            foreach (var item in Cart)
            {
                var product = await _productEndPoint.GetProductById(item.Product.Id);
                product.QuantityInStock -= item.QuantityInCart;

            }

            await ResetSalesViewModel();


        }

    }
}
