using Calculator.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    class CalcViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private CalcOperator _selectedOperator;

        public CalcOperator SelectedOperator
        {
            get { return _selectedOperator; }
            set
            {
                _selectedOperator = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Display");
            }
        }

        public string Display
        {
            get
            {
                switch (_selectedOperator)
                {
                    case CalcOperator.Addition:
                        return $"{LastNo}+{CurrNo}";
                    case CalcOperator.Substraction:
                        return $"{LastNo}-{CurrNo}";
                    case CalcOperator.Multiplication:
                        return $"{LastNo}*{CurrNo}";
                    case CalcOperator.Division:
                        return $"{LastNo}/{CurrNo}";
                    case CalcOperator.Undefined:
                    default:
                        return $"{LastNo}";
                }
            }
        }

        private string _lastNo;
        public string LastNo
        {
            get { return _lastNo; }
            set { _lastNo = value; NotifyPropertyChanged("Display"); }
        }

        private string _currNo;
        public string CurrNo
        {
            get { return _currNo; }
            set { _currNo = value; NotifyPropertyChanged("Display"); }
        }

        public CalcViewModel()
        {
            SelectedOperator = CalcOperator.Undefined;
            LastNo = "0";
            CurrNo = "0";
        }

        private ICommand _clearAllCommand;
        public ICommand ClearAllCommand
        {
            get
            {
                if (_clearAllCommand == null)
                    _clearAllCommand = new RelayCommand(ClearAllExecute, CanClearAllExecute, false);
                return _clearAllCommand;
            }
        }
        private bool CanClearAllExecute(object parameter)
        {
            return LastNo != "0" || CurrNo != "0" || SelectedOperator != CalcOperator.Undefined;
        }
        private void ClearAllExecute(object parameter)
        {
            SelectedOperator = CalcOperator.Undefined;
            LastNo = "0";
            CurrNo = "0";
        }

        private ICommand _toggleNegativeCommand;
        public ICommand ToggleNegativeCommand
        {
            get
            {
                if (_toggleNegativeCommand == null)
                    _toggleNegativeCommand = new RelayCommand(ToggleNegativeCommandExecute, CanToggleNegativeCommandExecute, false);
                return _toggleNegativeCommand;
            }
        }
        private bool CanToggleNegativeCommandExecute(object arg)
        {
            return true;
        }
        private void ToggleNegativeCommandExecute(object obj)
        {
            if (SelectedOperator == CalcOperator.Undefined)
                LastNo = (double.Parse(LastNo) * -1).ToString();
            else
                CurrNo = (double.Parse(CurrNo) * -1).ToString();
        }

        private ICommand _calcPercentageCommand;
        public ICommand CalcPercentageCommand
        {
            get
            {
                if (_calcPercentageCommand == null)
                    _calcPercentageCommand = new RelayCommand(CalcPercentageExecute, CanCalcPercentageExecute, false);
                return _calcPercentageCommand;
            }
        }
        private bool CanCalcPercentageExecute(object arg)
        {
            return LastNo != "0";
        }
        private void CalcPercentageExecute(object obj)
        {
            double temp = double.Parse(LastNo);
            if (temp == 0) return;
            if (SelectedOperator != CalcOperator.Undefined && CurrNo != "0")
            {
                temp *= double.Parse(CurrNo);
                CurrNo = $"{ temp / 100}";
            }
            else
            {
                LastNo = $"{temp / 100}";
            }
        }

        private ICommand _insertOperatorCommand;
        public ICommand InsertOperatorCommand
        {
            get
            {
                if (_insertOperatorCommand == null)
                    _insertOperatorCommand = new RelayCommand(InsertOperatorCommandExecute, CanInsertOperatorCommandExecute, false);
                return _insertOperatorCommand;
            }
        }
        private bool CanInsertOperatorCommandExecute(object arg)
        {
            return SelectedOperator == CalcOperator.Undefined;
        }
        private void InsertOperatorCommandExecute(object obj)
        {
            //TODO:
            SelectedOperator = (CalcOperator)obj;
        }

        private ICommand _insertNumberCommand;
        public ICommand InsertNumberCommand
        {
            get
            {
                if (_insertNumberCommand == null)
                    _insertNumberCommand = new RelayCommand(InsertNumberExecute, CanInsertNumberExecute, false);
                return _insertNumberCommand;
            }
        }
        private bool CanInsertNumberExecute(object arg)
        {
            return true;
        }
        private void InsertNumberExecute(object obj)
        {
            //TODO
            if (SelectedOperator != CalcOperator.Undefined)
                CurrNo = (CurrNo == "0") ? $"{(int)obj}" : $"{CurrNo}{(int)obj}";
            else
                LastNo = (LastNo == "0") ? $"{(int)obj}" : $"{LastNo}{(int)obj}";
        }

        private ICommand _insertDotCommand;
        public ICommand InsertDotCommand
        {
            get
            {
                if (_insertDotCommand == null)
                    _insertDotCommand = new RelayCommand(InsertDotCommandExecute, CanInsertDotCommandExecute, false);
                return _insertDotCommand;
            }
        }
        private bool CanInsertDotCommandExecute(object arg)
        {
            if (SelectedOperator == CalcOperator.Undefined)
                return !LastNo.Contains(".");
            else
                return CurrNo.Contains(".");
        }
        private void InsertDotCommandExecute(object obj)
        {
            if (SelectedOperator == CalcOperator.Undefined)
                LastNo += ".";
            else
                CurrNo += ".";
        }

        private ICommand _evaluateCommand;
        public ICommand EvaluateCommand
        {
            get
            {
                if (_evaluateCommand == null)
                    _evaluateCommand = new RelayCommand(EvaluateCommandExecute, CanEvaluateCommandExecute, false);
                return _evaluateCommand;
            }
        }
        private bool CanEvaluateCommandExecute(object arg)
        {
            if (SelectedOperator == CalcOperator.Undefined ||
                (SelectedOperator == CalcOperator.Division && CurrNo == "0"))
                return false;
            return true;
        }
        private void EvaluateCommandExecute(object obj)
        {
            double x = double.Parse(LastNo), y = double.Parse(CurrNo);
            switch (SelectedOperator)
            {
                case CalcOperator.Addition:
                    LastNo = $"{x + y}";
                    break;
                case CalcOperator.Substraction:
                    LastNo = $"{x - y}";
                    break;
                case CalcOperator.Multiplication:
                    LastNo = $"{x * y}";
                    break;
                case CalcOperator.Division:
                    LastNo = $"{x / y}";
                    break;
            }

            SelectedOperator = CalcOperator.Undefined;
            CurrNo = "0";
        }
    }

    public enum CalcOperator { Undefined, Addition, Substraction, Multiplication, Division }
    public enum CalcNumbers { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine }

}
