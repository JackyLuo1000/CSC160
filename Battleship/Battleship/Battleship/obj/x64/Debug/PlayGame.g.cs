﻿#pragma checksum "C:\Users\Sagebrecht\Google Drive\Neumont\CSC160 (C# & .NET)\Battleship\Battleship\Battleship\PlayGame.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0FE0C3D7E0F9E27AD6AEF70E5078F268"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Battleship
{
    partial class PlayGame : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // PlayGame.xaml line 23
                {
                    global::Windows.UI.Xaml.Input.KeyboardAccelerator element2 = (global::Windows.UI.Xaml.Input.KeyboardAccelerator)(target);
                    ((global::Windows.UI.Xaml.Input.KeyboardAccelerator)element2).Invoked += this.CheatMode_Invoked;
                }
                break;
            case 3: // PlayGame.xaml line 24
                {
                    global::Windows.UI.Xaml.Input.KeyboardAccelerator element3 = (global::Windows.UI.Xaml.Input.KeyboardAccelerator)(target);
                    ((global::Windows.UI.Xaml.Input.KeyboardAccelerator)element3).Invoked += this.ComputerWins_Invoked;
                }
                break;
            case 4: // PlayGame.xaml line 44
                {
                    this.PlayerGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 5: // PlayGame.xaml line 265
                {
                    this.ComputerGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 6: // PlayGame.xaml line 486
                {
                    this.SaveGame = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.SaveGame).Click += this.SaveGame_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

