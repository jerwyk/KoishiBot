using KoishiBot.MTG.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishiBot.MTG.Services
{
    public class ttsClient
    {

        private string _cardBack = "https://s3.amazonaws.com/frogtown.cards.hq/CardBack.jpg";

        /// <summary>
        /// Creates a tts card from mtg card object
        /// </summary>
        /// <param name=card></param>
        /// <returns></returns>
        public string CreateSimpleCard(SimpleCard card)
        {

            Dictionary<int, object> custom = new Dictionary<int, object>();

            custom.Add(1, new
            {
                FaceURL = card.ImageUrl,
                BackURL = this._cardBack,
                NumWidth = 1,
                NumHeight = 1,
                BackIsHidden = true,
                UniqueBack = false,
                Type = 0
            });

            var obj = new
            {
                Gravity = 0.5,
                PlayArea = 0.5,
                ObjectStates = new[]
                {
                    new
                    {
                        Name = "Card",
                        Transform= new
                        {
                            posX= 0,
                            posY= 0,
                            posZ= 0,
                            rotX= 0,
                            rotY= 0,
                            rotZ= 0,
                            scaleX= 1.0,
                            scaleY= 1.0,
                            scaleZ= 1.0
                        },
                        Nickname= card.Name,
                        Description= "",
                        GMNotes= "",
                        ColorDiffuse= new
                        {
                            r= 0,
                            g= 0,
                            b= 0
                        },
                        Locked= false,
                        Grid= true,
                        Snap= true,
                        IgnoreFoW= false,
                        MeasureMovement= false,
                        DragSelectable= true,
                        Autoraise= true,
                        Sticky= true,
                        Tooltip= true,
                        GridProjection= false,
                        HideWhenFaceDown= true,
                        Hands= true,
                        CardID= 100,
                        SidewaysCard= false,
                        CustomDeck= custom
                    }
                }
            };

            return JsonConvert.SerializeObject(obj);

        }

    }

}
