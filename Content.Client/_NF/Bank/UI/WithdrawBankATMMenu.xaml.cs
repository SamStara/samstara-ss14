using Content.Client.UserInterface.Controls;
using Content.Shared._NF.Bank;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client._NF.Bank.UI;

[GenerateTypedNameReferences]
public sealed partial class WithdrawBankATMMenu : FancyWindow
{
    public Action? WithdrawRequest;
    public Action? DepositRequest;
    public int Amount;
    public WithdrawBankATMMenu()
    {
        RobustXamlLoader.Load(this);
        WithdrawButton.OnPressed += OnWithdrawPressed;
        Title = Loc.GetString("bank-atm-menu-title");
        WithdrawEdit.OnTextChanged += OnAmountChanged;
    }

    public void SetBalance(int amount)
    {
        BalanceLabel.Text = BankSystemExtensions.ToSpesoString(amount);
    }

    public void SetEnabled(bool enabled)
    {
        WithdrawButton.Disabled = !enabled;
    }

    private void OnWithdrawPressed(BaseButton.ButtonEventArgs obj)
    {
        WithdrawRequest?.Invoke();
    }

    private void OnAmountChanged(LineEdit.LineEditEventArgs args)
    {
        if (int.TryParse(args.Text, out var amount))
        {
            Amount = amount;
        }
    }
}
