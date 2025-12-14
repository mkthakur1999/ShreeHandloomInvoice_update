using ShreeHandloomInvoice.ViewModel;

public class InvoiceItemModel : BaseViewModel
{
    private int _srNo;
    public int SrNo
    {
        get => _srNo;
        set => SetProperty(ref _srNo, value);
    }

    private string _description = "";
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private string _hsn = "";
    public string HSN
    {
        get => _hsn;
        set => SetProperty(ref _hsn, value);
    }

    private double _quantity;
    public double Quantity
    {
        get => _quantity;
        set
        {
            if (SetProperty(ref _quantity, value))
                Amount = Rate * Quantity;
        }
    }

    private string _unit = "";
    public string Unit
    {
        get => _unit;
        set => SetProperty(ref _unit, value);
    }

    private double _rate;
    public double Rate
    {
        get => _rate;
        set
        {
            if (SetProperty(ref _rate, value))
                Amount = Rate * Quantity;
        }
    }

    private double _amount;
    public double Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }
}
