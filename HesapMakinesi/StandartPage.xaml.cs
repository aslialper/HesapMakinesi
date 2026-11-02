namespace HesapMakinesi;

public partial class StandartPage : ContentPage
{
    private double firstValue = 0;
    private string activeOperator = "";
    private bool isNewEntry = true;

    public StandartPage()
    {
        InitializeComponent();
    }
    private void OnNumberClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;

        if (ResultLabel.Text == "0" || ResultLabel.Text == "Hata" || ResultLabel.Text == "Sıfıra bölünemez")
        {
            ResultLabel.Text = pressed;
        }
        else
        {
            ResultLabel.Text += pressed;
        }
    }

    private void OnDecimalClicked(object sender, EventArgs e)
    {
        ResultLabel.Text += ",";
    }

    private void OnOperatorClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string operatorText = button.Text; 

        // işlem butonunu ekle sayfaya
        ResultLabel.Text += " " + operatorText + " ";
    }

    private void OnEqualClicked(object sender, EventArgs e)
    {
        try
        {
            string expression = ResultLabel.Text;
            // kod x ne anlamaz * bilir aynı şekilde ÷ ne anlamaz o yüzden slaş kodun hangi işlemi istediğimizi anlmasını sağlamak için
            expression = expression.Replace("×", "*").Replace("÷", "/").Replace(",", ".");

            var result = new System.Data.DataTable().Compute(expression, null);
            ResultLabel.Text = result.ToString();
        }
        catch (Exception)
        {
            ResultLabel.Text = "Hata";
        }
    }

    private void OnSpecialOperatorClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (!double.TryParse(ResultLabel.Text, out double val)) return;

        switch (button.Text)
        {
            case "%":
                ResultLabel.Text = (val / 100).ToString();
                break;
            case "1/x":
                if (val == 0)
                {
                    ResultLabel.Text = "Sıfıra bölünemez";
                    isNewEntry = true;
                    return;
                }
                ResultLabel.Text = (1 / val).ToString();
                break;
            case "x²":
                ResultLabel.Text = (val * val).ToString();
                break;
            case "√x":
                if (val < 0)
                {
                    ResultLabel.Text = "Geçersiz giriş";
                    isNewEntry = true;
                    return;
                }
                ResultLabel.Text = Math.Sqrt(val).ToString();
                break;
        }
        isNewEntry = true;
    }

    private void OnNegateClicked(object sender, EventArgs e)
    {
        if (double.TryParse(ResultLabel.Text, out double val))
        {
            ResultLabel.Text = (-val).ToString();
        }
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        firstValue = 0;
        activeOperator = "";
        ResultLabel.Text = "0";
        isNewEntry = true;
    }

    private void OnClearEntryClicked(object sender, EventArgs e)
    {
        ResultLabel.Text = "0";
        isNewEntry = true;
    }

    private void OnBackspaceClicked(object sender, EventArgs e)
    {
        if (isNewEntry || ResultLabel.Text.Length <= 1)
        {
            ResultLabel.Text = "0";
            isNewEntry = true;
        }
        else
        {
            ResultLabel.Text = ResultLabel.Text.Substring(0, ResultLabel.Text.Length - 1);
        }
    }
}