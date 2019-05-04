﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus {

    public class EventListener
    {
        public delegate void Callback();
        public bool IsSingleShot;
        public Callback Method;

        public EventListener()
        {
            IsSingleShot = false;
        }
    }
    private Dictionary<string, IList<EventListener>> m_EventTable;
    private Dictionary<string, IList<EventListener>> EventTable
    {
        get
        {
            if (m_EventTable == null)
            {
                m_EventTable = new Dictionary<string, IList<EventListener>>();
            }
            return m_EventTable;
        }
    }
    public void AddListener(string name,EventListener.Callback method)
    {
        AddListener(name, new EventListener { Method = method });
    }
    public void AddListener(string name,EventListener listener)
    {
        if(!EventTable.ContainsKey(name))
        {
            EventTable.Add(name, new List<EventListener>());
        }
        if(EventTable[name].Contains(listener))
        {
            return;
        }
        EventTable[name].Add(listener);
    }
    public void RaiseEvent(string name)
    {
        if(!EventTable.ContainsKey(name))
        {
            return;
        }
        for(int i=0;i<EventTable[name].Count;i++)
        {
            EventListener listener = EventTable[name][i];
            listener.Method();
            if(listener.IsSingleShot)
            {
                EventTable[name].Remove(listener);
            }
        }
    }
}
