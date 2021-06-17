using App_FourSquare_V.Models;
using App_FourSquare_V.Renders;
using App_FourSquare_V.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MyMap), typeof(MyMapRender))]
namespace App_FourSquare_V.UWP.Renders
{
    public class MyMapRender : MapRenderer
    {
        MapControl NativeMap; FQModel FQ;
        MapWindow FQWindow; //Agregar UserControl FQWindow en carpeta global UWP 
        bool IsVisible = false;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

                if (e.OldElement != null)
                {
                    NativeMap.MapElementClick -= OnMapElementClick;
                    NativeMap.Children.Clear();
                    NativeMap = null;
                    FQWindow = null;
                }

                if (e.NewElement != null)
                {
                    FQ = (e.NewElement as MyMap).FQ;
                    var formMap = (MyMap)e.NewElement;
                    NativeMap = Control as MapControl;
                    NativeMap.Children.Clear();
                    NativeMap.MapElementClick += OnMapElementClick;

                    var position = new BasicGeoposition
                    {
                        Latitude = FQ.Latitude,
                        Longitude = FQ.Longitude
                    };

                    var point = new Geopoint(position);

                    var mapIcon = new MapIcon();
                    mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///pin.png"));

                    //Agregar PNG en la carpeta general de UWP

                    mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                    mapIcon.Location = point;
                    mapIcon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0);

                    NativeMap.MapElements.Add(mapIcon);
                }
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            if(mapIcon != null)
            {
                if(!IsVisible)
                {
                    if (FQWindow == null) FQWindow = new MapWindow(FQ);

                    var position = new BasicGeoposition
                    {
                        Latitude = FQ.Latitude,
                        Longitude = FQ.Longitude
                    };

                    var point = new Geopoint(position);

                    NativeMap.Children.Add(FQWindow);
                    MapControl.SetLocation(FQWindow, point);
                    MapControl.SetNormalizedAnchorPoint(FQWindow, new Windows.Foundation.Point(0.5, 1.0));

                    IsVisible = true;
                }
                else
                {
                    NativeMap.Children.Remove(FQWindow);
                    IsVisible = false;
                }
            }
        }


    }
}
