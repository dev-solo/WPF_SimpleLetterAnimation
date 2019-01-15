using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LetterAnimation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private string[] colors = new string[]
        {
            "#FBDB4A", "#F3934A", "#EB547D", "#9F6AA7", "#5476B3", "#2BB19B", "#70B984"
        };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;
            
            if (text == "")
            {
                TextViewer.Text = text.ToUpper();
                return;
            }
            if (text.Length < TextViewer.Text.Length)
                DeleteEffectFromLastChar(TextViewer, text);
            else
                AddEffectToLastChar(TextViewer, text);
            TextViewer.Text = text.ToUpper();
        }
        private void DeleteEffectFromLastChar(TextBlock element, string text)
        {
            Storyboard.SetTargetProperty(JumpAnimation, new PropertyPath("TextEffects[" + (text.Length - 1) + "].Transform.Children[0].Y"));
            Storyboard.SetTargetProperty(RotateAnimation, new PropertyPath("TextEffects[" + (text.Length - 1) + "].Transform.Children[1].Angle"));
            element.TextEffects.RemoveAt(element.TextEffects.Count - 1);
        }
        private void AddEffectToLastChar(TextBlock element, string text)
        {
            if (text.Length == element.TextEffects.Count)
                return;
            TextEffect textEffect = new TextEffect();
            Random random = new Random();
            BrushConverter brushConverter = new BrushConverter();
            textEffect.Foreground = (Brush)brushConverter.ConvertFromString(colors[random.Next(colors.Length)]);
            textEffect.PositionStart = text.Length - 1;
            textEffect.PositionCount = 1;
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(new TranslateTransform());
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.CenterX = (TextViewer.ActualWidth / text.Length) * text.Length;
            rotateTransform.CenterY = TextViewer.ActualHeight / 2;
            transformGroup.Children.Add(rotateTransform);
            textEffect.Transform = transformGroup;
            element.TextEffects.Add(textEffect);
            Storyboard.SetTargetProperty(JumpAnimation, new PropertyPath("TextEffects[" + (text.Length - 1) + "].Transform.Children[0].Y"));
            Storyboard.SetTargetProperty(RotateAnimation, new PropertyPath("TextEffects[" + (text.Length - 1) + "].Transform.Children[1].Angle"));
        }
    }
}
