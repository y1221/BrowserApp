using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace BrowserApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Uri _newUri;
        private const string SEARCH_URI = "https://google.com/search?q=";

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void GoButton_Click(object sender, RoutedEventArgs e)
        {
            if (Uri.TryCreate(TextBox1.Text, UriKind.Absolute, out _newUri) && _newUri.Scheme.StartsWith("http"))
            {
                WebView1.Navigate(_newUri);
            }
            else
            {
                if (Uri.TryCreate(SEARCH_URI + TextBox1.Text, UriKind.Absolute, out _newUri))
                {
                    WebView1.Navigate(_newUri);
                }
                else
                {
                    await new MessageDialog("不正なURIです。").ShowAsync();
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            WebView1.GoBack();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            WebView1.Refresh();
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            WebView1.GoForward();
        }

        // 画面遷移時、URIをアドレスバーに表示
        private void WebView1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            TextBox1.Text = WebView1.Source.ToString();
        }

        // アドレスバーカーソル時に全選択にする
        private void TextBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox1.SelectAll();
        }
    }
}
