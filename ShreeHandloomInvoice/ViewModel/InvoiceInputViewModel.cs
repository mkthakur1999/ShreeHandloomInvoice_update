using ShreeHandloomInvoice.Command;
using ShreeHandloomInvoice.Converter;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShreeHandloomInvoice.ViewModel
{
    public class InvoiceInputViewModel : BaseViewModel
    {
        public InvoiceInputViewModel()
        {
            Invoice = new InvoiceModel();

            NewItem = new InvoiceItemModel();

            AddItemCommand = new RelayCommand(AddItem);
            GenerateInvoiceCommand = new RelayCommand(GenerateInvoice);
        }

        // ---------------- INVOICE MAIN MODEL ----------------
        private InvoiceModel _invoice;
        public InvoiceModel Invoice
        {
            get => _invoice;
            set => SetProperty(ref _invoice, value);
        }

        // ---------------- NEW ITEM INPUT MODEL ----------------
        private InvoiceItemModel _newItem;
        public InvoiceItemModel NewItem
        {
            get => _newItem;
            set => SetProperty(ref _newItem, value);
        }

        // Commands
        public ICommand AddItemCommand { get; }
        public ICommand GenerateInvoiceCommand { get; }

        // ---------------- ADD ITEM ----------------
        private void AddItem()
        {
            // Validate
            if (string.IsNullOrWhiteSpace(NewItem.Description)) return;
            if (NewItem.Quantity <= 0) return;

            var item = new InvoiceItemModel
            {
                SrNo = Invoice.Items.Count + 1,
                Description = NewItem.Description,
                HSN = NewItem.HSN,
                Quantity = NewItem.Quantity,
                Rate = NewItem.Rate,
                Unit = NewItem.Unit,
                Amount = NewItem.Quantity * NewItem.Rate,
                
                
            };
            //var itm = new InvoiceModel
            //{
            //    ConsigneeName = Invoice.ConsigneeName
            //};
            Invoice.Items.Add(item);

            // Clear input fields
            NewItem = new InvoiceItemModel();
            OnPropertyChanged(nameof(NewItem));
        }

        // ---------------- GENERATE INVOICE ----------------
        private void GenerateInvoice()
        {
            // Auto calculate amount for each item
            foreach (var item in Invoice.Items)
            {
                item.Amount = item.Quantity * item.Rate;
            }

            // Total sum
            Invoice.TotalAmount = 0;
            foreach (var item in Invoice.Items)
                Invoice.TotalAmount += item.Amount;

            // Convert total to words
            Invoice.AmountInWords = AmountToWords.Convert(Invoice.TotalAmount);

            // Navigate to Preview Page
            NavigationService.Navigate(new InvoicePreviewViewModel(Invoice));
        }
    }
}
