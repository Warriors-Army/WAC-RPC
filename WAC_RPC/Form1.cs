using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;
using IWshRuntimeLibrary;

namespace WAC_RPC
{
    public partial class setup : Form
    {
        bool clickDefault = false; // faire des vérifications sur l'état du bouton default
        bool clickCustom = false; // faire des vérifications sur l'état du bouton custom
        ComponentResourceManager resources = new ComponentResourceManager(typeof(setup)); // un code que j'ai copier quelques part mdr
        // création des clients Default et Custom
        DiscordRpcClient clientDefault;
        DiscordRpcClient clientCustom;

        public setup()
        {
            InitializeComponent(); // généré par VS

            // Animation du bouton default
            defaut.MouseEnter += (object sender, EventArgs e) =>
            {
                // si le bouton est actif
                if (clickDefault)
                {
                    // on le met vert claire
                    defaut.BackgroundImage = ressources.Images[ressources.Images.IndexOfKey("default_rpc_active_hover.png")];
                    // sinon
                }
                else
                {
                    // on le met gris foncé
                    defaut.BackgroundImage = ressources.Images[ressources.Images.IndexOfKey("default_rpc_hover.png")];
                }
            };
            defaut.MouseLeave += (object sender, EventArgs e) =>
            {
                // si le bouton est actif
                if (clickDefault)
                {
                    // on le met vert foncé
                    defaut.BackgroundImage = ressources.Images[ressources.Images.IndexOfKey("default_rpc_active.png")];
                    // sinon
                }
                else
                {
                    // on le met gris claire
                    defaut.BackgroundImage = ((Image)(resources.GetObject("defaut.BackgroundImage")));
                }
            };
            // Animation du bouton custom
            custom.MouseEnter += (object sender, EventArgs e) =>
            {
                // si le bouton est actif
                if (clickCustom)
                {
                    // on le met vert claire
                    custom.BackgroundImage = ressources.Images[ressources.Images.IndexOfKey("custom_rpc_active_hover.png")];
                    // sinon
                }
                else
                {
                    // on le met gris foncé
                    custom.BackgroundImage = ressources.Images[ressources.Images.IndexOfKey("custom_rpc_hover.png")];
                }
            };
            custom.MouseLeave += (object sender, EventArgs e) =>
            {
                // si le bouton est actif
                if (clickCustom)
                {
                    // on le met vert foncé
                    custom.BackgroundImage = ressources.Images[ressources.Images.IndexOfKey("custom_rpc_active.png")];
                    // sinon
                }
                else
                {
                    // on le met gris claire
                    custom.BackgroundImage = ((Image)(resources.GetObject("custom.BackgroundImage")));
                }
            };

            // Initialisation des menu select
            largeImg.SelectedItem = "wac_logo";
            smallImg.SelectedItem = "fc_wac";

            // création du racoucrcis sur le bureau
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + '\\' + Application.ProductName + ".lnk";
            var shell = new WshShell();
            var shortcut = shell.CreateShortcut(path) as IWshShortcut;
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.IconLocation = Application.ExecutablePath;
            shortcut.Description = "Warriors Rich Presence";
            shortcut.Save();
        }

        // Event activé quand on clique sur le bouton default
        private void defaut_Click(object sender, EventArgs e)
        {
            // quand on clique on inverse l'état du bouton default
            clickDefault = !clickDefault;

            // si il est actif
            if (clickDefault)
            {
                // on desactive le bouton custom
                custom.Enabled = false;
                // on met l'image du bouton "active"
                defaut.BackgroundImage = ressources.Images[ressources.Images.IndexOfKey("default_rpc_active_hover.png")];

                // ---- on set le RPC ----
                // initialisation des variables
                string url1 = urlBtn1.Text.ToString();
                string url2 = urlBtn2.Text.ToString();
                string large = largeImg.SelectedItem.ToString();
                string small = smallImg.SelectedItem.ToString();
                
                // création du RPC
                RichPresence presence = new RichPresence()
                {
                    Details = details.Text.ToString(),
                    State = stats.Text.ToString()
                };

                Uri uriResult; // check si l'url est de la bonne forme
                bool checkBtn = true; // check si les boutons sont correctement set
                int numbBtn = 0; // nombre de boutons
                // si les 2 boutons sont set
                if(txtBtn1.Text.ToString().Length > 0 && txtBtn2.Text.ToString().Length > 0)
                {
                    numbBtn = 2; // il y a 2 boutons
                // sinon, si un des deux boutons est set mais pas l'autre
                } else if ((txtBtn1.Text.ToString().Length > 0 && txtBtn2.Text.ToString().Length == 0) || (txtBtn1.Text.ToString().Length == 0 && txtBtn2.Text.ToString().Length > 0))
                {
                    // il n'y a qu'un bouton
                    numbBtn = 1;
                // sinon il n'y a pas de boutons (variable set à 0)
                }

                // si il y a au moins un bouton
                if (numbBtn > 0)
                {
                    // on créer un tableau de boutons avec le nombre de boutons set
                    DiscordRPC.Button[] buttons = new DiscordRPC.Button[numbBtn];
                    // si le premier bouton est set
                    if (txtBtn1.Text.ToString().Length > 0)
                    {
                        // si l'url est corectement set
                        if (Uri.TryCreate(url1, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                        {
                            // on créer un bouton à la première position du tableau
                            buttons[0] = new DiscordRPC.Button
                            {
                                // avec un label et une URL
                                Label = txtBtn1.Text.ToString(),
                                Url = url1
                            };
                        }
                        // sinon on met checkBtn à false (pour la suite)
                        else
                        {
                            checkBtn = false;
                        }
                    }

                    // si le deuxième bouton est set
                    if (txtBtn2.Text.ToString().Length > 0)
                    {
                        // si l'url est correctement set
                        if (Uri.TryCreate(url2, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                        {
                            // on créer un bouton au dernier emplacement du tableau dispo
                            buttons[numbBtn-1] = new DiscordRPC.Button
                            {
                                // avec un label et une URL
                                Label = txtBtn2.Text.ToString(),
                                Url = url2
                            };
                        }
                        // sinon on met checkBtn à false (pour la suite)
                        else
                        {
                            checkBtn = false;
                        }
                    }
                    // on set les buttons du presence avec le(s) bouton(s) qu'on vient de créer
                    presence.Buttons = buttons;
                }

                // création des assets
                Assets assets = new Assets();

                // si une image large est choisie
                if (!large.Equals("none"))
                {
                    // on la set avec le texte donné par l'utilisateur
                    assets.LargeImageKey = large;
                    assets.LargeImageText = largeTxt.Text.ToString();
                }

                // si une image small est choisie
                if (!small.Equals("none"))
                {
                    // on la set avec le texte donné par l'utilisateur
                    assets.SmallImageKey = small;
                    assets.SmallImageText = smallTxt.Text.ToString();
                }
                // on set les assets avec le(s) image(s) qu'on vient de créer
                presence.Assets = assets;

                // si les boutons sont correctement set
                if (checkBtn)
                {
                    // on set le presence
                    clientDefault = startRpc("815958403259695104", presence);
                // sinon
                } else
                {
                    // on dit que y a un problème
                    MessageBox.Show("Vous devez passer une URL valide pour votre bouton.", "Error", MessageBoxButtons.OK);
                }
            // si il est pas actif
            } else
            {
                // on met une image de bouton pas actif
                defaut.BackgroundImage = ressources.Images[0];
                // on active le bouton custom
                custom.Enabled = true;

                // on clear le Presence
                endRpc(clientDefault);
            }

        }

        private void custom_Click(object sender, EventArgs e)
        {
            // quand on clique on inverse l'état du bouton default
            clickCustom = !clickCustom;

            // si il est actif
            if (clickCustom)
            {
                // on désactive le bouton default
                defaut.Enabled = false;
                // on met l'image du bouton "active"
                custom.BackgroundImage = ressources.Images[ressources.Images.IndexOfKey("custom_rpc_active_hover.png")];

                // initialisation d'un nouveau client

                // ---- on set le RPC ----
                // initialisation des variables
                string url1 = urlBtn1.Text.ToString();
                string url2 = urlBtn2.Text.ToString();
                string large = largeImg2.Text.ToString();
                string small = smallImg2.Text.ToString();

                // création du RPC
                RichPresence presenceCustom = new RichPresence()
                {
                    Details = details.Text.ToString(),
                    State = stats.Text.ToString()
                };

                Uri uriResult; // check si l'url est de la bonne forme
                bool checkBtn = true; // check si les boutons sont correctement set
                int numbBtn = 0; // nombre de boutons
                // si les 2 boutons sont set
                if (txtBtn1.Text.ToString().Length > 0 && txtBtn2.Text.ToString().Length > 0)
                {
                    numbBtn = 2; // il y a 2 boutons
                                 // sinon, si un des deux boutons est set mais pas l'autre
                }
                else if ((txtBtn1.Text.ToString().Length > 0 && txtBtn2.Text.ToString().Length == 0) || (txtBtn1.Text.ToString().Length == 0 && txtBtn2.Text.ToString().Length > 0))
                {
                    // il n'y a qu'un bouton
                    numbBtn = 1;
                    // sinon il n'y a pas de boutons (variable set à 0)
                }

                // si il y a au moins un bouton
                if (numbBtn > 0)
                {
                    // on créer un tableau de boutons avec le nombre de boutons set
                    DiscordRPC.Button[] buttons = new DiscordRPC.Button[numbBtn];
                    // si le premier bouton est set
                    if (txtBtn1.Text.ToString().Length > 0)
                    {
                        // si l'url est corectement set
                        if (Uri.TryCreate(url1, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                        {
                            // on créer un bouton à la première position du tableau
                            buttons[0] = new DiscordRPC.Button
                            {
                                // avec un label et une URL
                                Label = txtBtn1.Text.ToString(),
                                Url = url1
                            };
                        }
                        // sinon on met checkBtn à false (pour la suite)
                        else
                        {
                            checkBtn = false;
                        }
                    }

                    // si le deuxième bouton est set
                    if (txtBtn2.Text.ToString().Length > 0)
                    {
                        // si l'url est correctement set
                        if (Uri.TryCreate(url2, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                        {
                            // on créer un bouton au dernier emplacement du tableau dispo
                            buttons[numbBtn - 1] = new DiscordRPC.Button
                            {
                                // avec un label et une URL
                                Label = txtBtn2.Text.ToString(),
                                Url = url2
                            };
                        }
                        // sinon on met checkBtn à false (pour la suite)
                        else
                        {
                            checkBtn = false;
                        }
                    }
                    // on set les buttons du presence avec le(s) bouton(s) qu'on vient de créer
                    presenceCustom.Buttons = buttons;
                }

                // création des assets
                Assets assets = new Assets();

                // si une image large est choisie
                if (!large.Equals("none"))
                {
                    // on la set avec le texte donné par l'utilisateur
                    assets.LargeImageKey = large;
                    assets.LargeImageText = largeTxt.Text.ToString();
                }

                // si une image small est choisie
                if (!small.Equals("none"))
                {
                    // on la set avec le texte donné par l'utilisateur
                    assets.SmallImageKey = small;
                    assets.SmallImageText = smallTxt.Text.ToString();
                }
                // on set les assets avec le(s) image(s) qu'on vient de créer
                presenceCustom.Assets = assets;

                // si les boutons sont correctement set
                if (checkBtn)
                {
                    if (!clientIdTxt.Text.ToString().Equals(""))
                    {
                        // on set le presence
                        clientCustom = startRpc(clientIdTxt.Text.ToString(), presenceCustom);
                        // sinon
                    }
                    else
                    {
                        MessageBox.Show("Vous devez renseigner l'ID de l'application Discord.", "Error", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    // on dit que y a un problème
                    MessageBox.Show("Vous devez passer une URL valide pour votre bouton.", "Error", MessageBoxButtons.OK);
                }
                // si il est pas actif
            }
            else
            {
                // on met une image de bouton pas actif
                custom.BackgroundImage = ressources.Images[ressources.Images.IndexOfKey("custom_rpc_hover.png")];
                // on réactive le bouton default
                defaut.Enabled = true;

                // on clear le Presence
                endRpc(clientCustom);
            }
        }

        // set le presence
        DiscordRpcClient startRpc(string clientID, RichPresence presence)
        {
            // initialisation du clien
            DiscordRpcClient client = new DiscordRpcClient(clientID);
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.Initialize();

            // affichange du prensence
            client.SetPresence(presence);
            return client;
        }

        // unset le presence
        void endRpc(DiscordRpcClient client)
        {
            try
            {
                // effaçage du presence
                client.ClearPresence();
                // fermeture du client
                client.Deinitialize();
                // p'tet ça marchera mieux
                client.Dispose();
            } catch(Exception e) { }
        }
    }
}

/*
                           ^
                          /|\    
                         / | \      
                         | | |     
                         | | |  
                         | | |    
                         | | |    
                         | | |  
                         | | |          
                         | | |    
                         | | |   
                         | | |   
                         | | |     
                         | | |  
                         | | |  
                         | | | 
                         | | |
                   /|    |_|_|    |\
                   \ \___/ W \___/ /
                    \_____ _ _____/
                          |-|
                          |-|
                          |-|
                         .'-'.
                         '---'
*/