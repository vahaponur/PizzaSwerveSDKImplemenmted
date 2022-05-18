using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class MobileNot : MonoBehaviour
{
    #region Serialized Fields
    #endregion

    #region Private Fields
    #endregion

    #region Public Properties
    #endregion

    #region MonoBehaveMethods
    void Awake()
    {
	
    }

    void Start()
    {
        //Remove notifications already displayed
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notification Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        
        var notification = new AndroidNotification();
        notification.Title = "Express Pizza";
        notification.Text = "You have pizzas to deal with, come on!";
        notification.SmallIcon = "not2";
        notification.LargeIcon = "not1";
        notification.FireTime = System.DateTime.Now.AddHours(8);

        var id  = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
        

    }

   
    void Update()
    {
        
    }
    #endregion
    
    #region PublicMethods
    #endregion
    
    #region PrivateMethods
    #endregion
}
