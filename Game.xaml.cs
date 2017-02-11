using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {

        private Stopwatch timer = new Stopwatch();
        private Windows.UI.Input.GestureRecognizer gr = new Windows.UI.Input.GestureRecognizer();
        private String[] text = new String[] { "Tap Once", "Tap Twice", "Tap Three times", "Stay Calm", "Please Tap", "I say tap Three times", "Don't touch!", "Tap more than once and less than thrice", "Swipe Left", "Swipe Right", "Don't Swipe", "Tap and Hold", "Swipe Down", "Swipe Up" };
        private Random rand = new Random();
        private int index;
        private static int score = 0;

        public Game()
        {
            this.InitializeComponent();
            timer.Reset();
            timer.Start();
            index = rand.Next();
            textBlock.Text = text[index % 3];

            // pointer press/release handlers
            button1.PointerPressed += new PointerEventHandler(target_PointerPressed);
            button1.PointerReleased += new PointerEventHandler(target_PointerReleased);

            // pointer enter/exit handlers
            button1.PointerEntered += new PointerEventHandler(target_PointerEntered);
            button1.PointerExited += new PointerEventHandler(target_PointerExited);

            // gesture handlers
            button1.Tapped += new TappedEventHandler(target_Tapped);
            button1.DoubleTapped += new DoubleTappedEventHandler(target_DoubleTapped);

            button1.Holding += new HoldingEventHandler(target_Holding);
            button1.RightTapped += new RightTappedEventHandler(target_RightTapped);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            timer.Reset();
            timer.Start();

        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            progressBar.Value = timer.Elapsed.Seconds;
            timer.Stop();
            timer.Reset();
            timer.Start();
            index = rand.Next();
            textBlock.Text = text[index % 14];
            MediaElement mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(textBlock.Text);
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
            textBlock1.Text = score.ToString();
            score += 1;
        }

        // A PointerPressed event is sent whenever a mouse button, finger, or pen is pressed to make
        // contact with an object
        void target_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            button1.Background = new SolidColorBrush(Windows.UI.Colors.RoyalBlue);
            //textBlock1.Text = "Pointer Pressed";
        }

        // A PointerReleased event is sent whenever a mouse button, finger, or pen is released to remove
        // contact from an object
        void target_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            button1.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
            //textBlock1.Text = "Pointer Released";
        }

        // A PointerEntered event is sent whenever a mouse cursor is moved on top of an object
        // or when a pen or finger is dragged on top of an object
        void target_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            button1.Background = new SolidColorBrush(Windows.UI.Colors.RoyalBlue);
            //textBlock1.Text = "Pointer Entered";
        }

        // A PointerExited event is sent whenever a mouse cursor is moved off of an object
        // or when a pen or finger is dragged off of an object
        void target_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            button1.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
            //textBlock1.Text = "Pointer Exited";
        }

        // A Tapped event is sent whenever a mouse is clicked or a finger or pen taps
        // the object
        void target_Tapped(object sender, TappedRoutedEventArgs e)
        {
            button1.Background = new SolidColorBrush(Windows.UI.Colors.DeepSkyBlue);
            //textBlock1.Text = "Tapped";
        }

        // A DoubleTapped event is sent whenever a mouse is double-clicked or a finger or pen taps
        // the object twice in quick succession
        void target_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            button1.Background = new SolidColorBrush(Windows.UI.Colors.RoyalBlue);
            //textBlock1.Text = "Double-Tapped";
        }

        // A RightTapped event is sent whenever a mouse is right-clicked or a finger or pen
        // completes a Holding event.  This is intended to be used to handle secondary actions
        // on an object.
        void target_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            button1.Background = new SolidColorBrush(Windows.UI.Colors.RoyalBlue);
            //textBlock1.Text = "Right Tapped";
        }

        // A Holding event is sent whenever a finger or pen is pressed and held on top of
        // an object.
        // Once a small amount of time has elapsed, the event is sent with a HoldingState
        // of the type HoldingState.Started, indicating that the held threshold has just
        // been passed.
        // When a finger has been lifted after a successful hold, a Holding event is sent
        // with a HoldingState of Completed. 
        // If the user cancels the hold after it has been started, but before it completes,
        // a Holding event is sent with a HoldingState of Canceled.
        void target_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == Windows.UI.Input.HoldingState.Started)
            {
                button1.Background = new SolidColorBrush(Windows.UI.Colors.DeepSkyBlue);
                //textBlock1.Text = "Holding";
            }
            else if (e.HoldingState == Windows.UI.Input.HoldingState.Completed)
            {
                button1.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
                //textBlock1.Text = "Held";
            }
            else
            {
                button1.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
                //textBlock1.Text = "Hold Canceled";
            }
        }

    }
}
