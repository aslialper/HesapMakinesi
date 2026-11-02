namespace HesapMakinesi;

public partial class ScientificPage : ContentPage
{
    private double firstValue = 0;
    private string activeOperator = "";
    private bool isNewEntry = true;

    public ScientificPage()
    {
        InitializeComponent();
    }

    private void OnNumberClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;

        if (ResultLabel.Text == "0" || isNewEntry || ResultLabel.Text == "Geçersiz giriş" || ResultLabel.Text == "Sıfıra bölünemez")
        {
            ResultLabel.Text = pressed;
            isNewEntry = false;
        }
        else
        {
            ResultLabel.Text += pressed;
        }
    }

    private void OnConstantClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (button.Text == "π")
            ResultLabel.Text = Math.PI.ToString();
        else if (button.Text == "e")
            ResultLabel.Text = Math.E.ToString();

        isNewEntry = true;
    }

    private void OnDecimalClicked(object sender, EventArgs e)
    {
        if (isNewEntry)
        {
            ResultLabel.Text = "0,";
            isNewEntry = false;
        }
        else if (!ResultLabel.Text.Contains(","))
        {
            ResultLabel.Text += ",";
        }
    }

    private void OnOperatorClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        if (double.TryParse(ResultLabel.Text, out double currentValue))
        {
            firstValue = currentValue;
            activeOperator = button.Text;
            isNewEntry = true;
        }
    }

    private void OnEqualClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(activeOperator)) return;

        if (double.TryParse(ResultLabel.Text, out double secondValue))
        {
            try
            {
                double result = Calculate(firstValue, secondValue, activeOperator);
                ResultLabel.Text = result.ToString();
                activeOperator = "";
                isNewEntry = true;
            }
            catch (DivideByZeroException)
            {
                ResultLabel.Text = "Sıfıra bölünemez";
                isNewEntry = true;
            }
            catch (Exception)
            {
                ResultLabel.Text = "Geçersiz giriş";
                isNewEntry = true;
            }
        }
    }

    private double Calculate(double val1, double val2, string op)
    {
        return op switch
        {
            "+" => val1 + val2,
            "-" => val1 - val2,
            "×" => val1 * val2,
            "÷" => val2 == 0 ? throw new DivideByZeroException() : val1 / val2,
            "x^y" => Math.Pow(val1, val2),
            "mod" => val2 == 0 ? throw new DivideByZeroException() : val1 % val2,
            _ => 0
        };
    }

    private void OnScientificOperatorClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (!double.TryParse(ResultLabel.Text, out double val)) 
                        return;

        try{
                 switch (button.Text){
                case "sin":
                    ResultLabel.Text = Math.Sin(val * Math.PI / 180.0).ToString();
                    break;
                case "cos":
                    ResultLabel.Text = Math.Cos(val * Math.PI / 180.0).ToString();
                    break;
                case "tan":
                    ResultLabel.Text = Math.Tan(val * Math.PI / 180.0).ToString();
                    break;
                case "x²":
                    ResultLabel.Text = (val * val).ToString();
                    break;
                case "1/x":
                    if (val == 0) throw new DivideByZeroException();
                    ResultLabel.Text = (1 / val).ToString();
                    break;
                case "√x":
                    if (val < 0) throw new ArgumentException();
                    ResultLabel.Text = Math.Sqrt(val).ToString();
                    break;
                case "|x|":
                    ResultLabel.Text = Math.Abs(val).ToString();
                    break;
                case "log":
                    if (val <= 0) throw new ArgumentException();
                    ResultLabel.Text = Math.Log10(val).ToString();
                    break;
                case "ln":
                    if (val <= 0) throw new ArgumentException();
                    ResultLabel.Text = Math.Log(val).ToString();
                    break;
                case "10^x":
                    ResultLabel.Text = Math.Pow(10, val).ToString();
                    break;
                case "exp":
                    ResultLabel.Text = Math.Exp(val).ToString();
                    break;
                case "n!":
                    if (val < 0 || val != Math.Floor(val)) throw new ArgumentException();
                    ResultLabel.Text = Factorial((int)val).ToString();
                    break;
            }
        }
        catch (DivideByZeroException){
            ResultLabel.Text = "Sıfıra bölünemez";
        }
        catch (Exception){
            ResultLabel.Text = "Geçersiz giriş";
        }
        isNewEntry = true;
    }

    private double Factorial(int n){
        if (n == 0 || n == 1) return 1;
        double fact = 1;
        for (int i = 2; i <= n; i++) fact *= i;
        return fact;
    }

    private void OnNegateClicked(object sender, EventArgs e)
    {
        if (double.TryParse(ResultLabel.Text, out double val))
        {
            ResultLabel.Text = (-val).ToString();
        }
    }

    private void OnClearClicked(object sender, EventArgs e){
        firstValue = 0;
        activeOperator = "";
        ResultLabel.Text = "0";
        isNewEntry = true;
    }

    private void OnBackspaceClicked(object sender, EventArgs e){
        if (isNewEntry || ResultLabel.Text.Length <= 1){
            ResultLabel.Text = "0";
            isNewEntry = true;
        }
        else
        {
            ResultLabel.Text = ResultLabel.Text.Substring(0, ResultLabel.Text.Length - 1);
        }


  
    
    
    
    
    
    
    
    }
}
