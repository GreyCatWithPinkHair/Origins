using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Sanford.Multimedia.Midi;

namespace Origins
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TogetherMIDI_Click(object sender, RoutedEventArgs e)
        {
            using var outDevice = new OutputDevice(0);
            var baseTone = new ChannelMessageBuilder
            {
                Command = ChannelCommand.NoteOn,
                MidiChannel = 0,
                Data1 = 55,
                Data2 = 100
            };

            var topTone = new ChannelMessageBuilder
            {
                Command = ChannelCommand.NoteOn,
                MidiChannel = 0,
                Data1 = 60,
                Data2 = 100
            };

            baseTone.Build();
            topTone.Build();
            outDevice.Send(baseTone.Result);
            outDevice.Send(topTone.Result);

            Thread.Sleep(1400);

            baseTone.Command = ChannelCommand.NoteOff;
            topTone.Command = ChannelCommand.NoteOff;
            baseTone.Data2 = 0;
            topTone.Data2 = 0;
            baseTone.Build();
            topTone.Build();
            outDevice.Send(baseTone.Result);
            outDevice.Send(topTone.Result);
        }

        private void SeparatelyMIDI_Click(object sender, RoutedEventArgs e)
        {
            using var outDevice = new OutputDevice(0);
            var baseTone = new ChannelMessageBuilder
            {
                Command = ChannelCommand.NoteOn,
                MidiChannel = 0,
                Data1 = 55,
                Data2 = 100
            };

            var topTone = new ChannelMessageBuilder
            {
                Command = ChannelCommand.NoteOn,
                MidiChannel = 0,
                Data1 = 60,
                Data2 = 100
            };

            baseTone.Build();
            outDevice.Send(baseTone.Result);

            Thread.Sleep(1250);

            baseTone.Command = ChannelCommand.NoteOff;
            baseTone.Data2 = 0;
            baseTone.Build();
            outDevice.Send(baseTone.Result);

            topTone.Build();
            outDevice.Send(topTone.Result);

            Thread.Sleep(1250);

            topTone.Command = ChannelCommand.NoteOff;
            topTone.Data2 = 0;
            topTone.Build();
            outDevice.Send(topTone.Result);
        }

        private void TogetherAudio_Click(object sender, RoutedEventArgs e)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var pathArray = currentDirectory.Split('\\');
            string? workDirectory = null;
            for (var i = 0; i < pathArray.Length - 3; i++)
            {
                workDirectory += pathArray[i] + '\\';
            }
            MediaPlayer first = new MediaPlayer();
            MediaPlayer second = new MediaPlayer();
            first.Open(new Uri(@$"{workDirectory}Resourses\piano-G3.wav", UriKind.Relative));
            second.Open(new Uri(@$"{workDirectory}Resourses\piano-C4.wav", UriKind.Relative));
            first.Play();
            second.Play();
            Thread.Sleep(1250);
            first.Stop();
            second.Stop();
        }

        private void SeparatelyAudio_Click(object sender, RoutedEventArgs e)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var pathArray = currentDirectory.Split('\\');
            string? workDirectory = null;
            for (var i = 0; i < pathArray.Length - 3; i++)
            {
                workDirectory += pathArray[i] + '\\';
            }
            
            MediaPlayer single = new MediaPlayer();
            single.Open(new Uri(@$"{workDirectory}Resourses\piano-G3.wav", UriKind.Relative));
            single.Play();
            Thread.Sleep(1245);

            single.Open(new Uri(@$"{workDirectory}Resourses\piano-C4.wav", UriKind.Relative));
            single.Play();
            Thread.Sleep(1250);
            single.Stop();
        }
    }
}
