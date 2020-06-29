using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Syntra.Data.Models;
using System.Windows.Shapes;
using WpfContactPersonen.ViewModel;
using Microsoft.Win32;

namespace WpfContactPersonen
{
   public class TempSelector:DataTemplateSelector
    {
        public override DataTemplate
                   SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is Contactpersoon)
            {
                Contactpersoon cp = item as Contactpersoon;

                if (cp.Categorie == "Fysieke contactpersoon")
                    return
                        element.FindResource("FysiekePersoonTemplate") as DataTemplate;
                else
                    return
                        element.FindResource("WinkelOfBedrijfTemplate") as DataTemplate;
            }

            return null;
        }
    }
}
