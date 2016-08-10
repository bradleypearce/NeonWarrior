using UnityEngine;
using System.Collections;
using System;

/// HEAP 
/// 
/// To optimise my pathfinding algorithm, i required a container class that 
/// sorted itself efficiently so the node with the lowest fcost could be retrieved 
/// as quickly as possible without iterating through every node in a single function. 
/// To make the class generic we use a parameterised type T which implements the IHeapItem interface.
/// This means that we can expand the use of this class to perform other expensive tasks which would otherwise
/// be impossible to implement in a 60FPS game. 
public class Heap<T> where T : IHeapItem<T>
{
    T[] m_items;
    int m_itemcount;

    /// CONSTRUCTOR
    /// 
    /// For this, we take in the maximum size this heap can be and create an array of type T the size of max size. 
    public Heap(int _maxsize)
    {
        m_items = new T[_maxsize];
    }

    /// ADD
    /// 
    /// This function takes in an item of type T. We then 
    /// set the index in the heap to our item count variable. 
    /// We then set the passed in item to the index itemcount in 
    /// our items array. We then call our sortUp function to resort the array
    /// and pass in our item. Then we increase our item count variable for the next 
    /// item. 
    public void Add(T _item)
    {
        _item.HeapIndex = m_itemcount;
        m_items[m_itemcount] = _item;
        SortUp(_item);
        m_itemcount++;
    }

    /// CONTAINS 
    /// 
    /// This function checks if the passed in item is 
    /// within the heap. For this we use the IComparable 
    /// interface function Equals and pass in our _item 
    /// and the item at the heapindex of our passed in item 
    /// in our item array. If equals returns true then the function 
    /// returns true.
    public bool Contains(T _item)
    {
        if (Equals(_item, m_items[_item.HeapIndex]))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// UPDATEITEM 
    /// 
    /// This function simply calls our private sortup function.
    public void UpdateItem(T _item)
    {
        SortUp(_item);
    }

    /// COUNT
    /// 
    /// This acts as a nice way of encapsulating our itemcount using a interface variable
    /// count and just returning our itemcount from here. 
    public int Count
    {
        get
        {
            return m_itemcount;
        }
    }

    /// SortDown
    /// 
    /// This function swaps compares the children of the item 
    /// you pass in and then compares the higher of the two children, if one is higher
    /// the swapindex is set and we call swap and pass in our item and our item at swap index in our array. 
    /// If the compareto function returns 0 we simply return from the function as the array is organised. 
    void SortDown(T _item)
    {
        while (true)
        {
            int childindexleft = _item.HeapIndex * 2 + 1;
            int childindexright = _item.HeapIndex * 2 + 2;

            int swapindex = 0;

            if (childindexleft < m_itemcount)
            {
                swapindex = childindexleft;

                 if (childindexright < m_itemcount)
                 {
                       if(m_items[childindexleft].CompareTo(m_items[childindexright]) < 0)
                       {
                             swapindex = childindexright;
                       }
                 }


                 if(_item.CompareTo(m_items[swapindex]) < 0)
                {
                    Swap(_item, m_items[swapindex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
            
        }
    }

    /// REMOVEFIRST 
    /// 
    /// This function returns an element from the array by firstly 
    /// setting the first item variable to the first element in items. 
    /// We then take away one from the itemcount. Then we set the first element 
    /// of our array to the back of the array. Set the heapindex to 0 and then call the sortdown
    /// function to reorganise our heap. We then return the first item we created at the start. 
    public T RemoveFirst()
    {
        T firstitem = m_items[0];
        m_itemcount--;
        m_items[0] = m_items[m_itemcount];
        m_items[0].HeapIndex = 0;
        SortDown(m_items[0]);
        return firstitem;
    }

    /// SORTUP 
    /// 
    /// This function takes in an item and first of all finds the parent index 
    /// (the heapindex of the item minus 1 as arrays are zero based and then divided by two.
    /// This gives us the parent of this element in the heap. Then we iterate continuously by firstly
    /// setting the parent item to our parentindex in our heap. Then we compare the item we passed in to the 
    /// parent item. If the function returns more than 0 we swap our item and parent item elements. Otherwise we break
    /// from the loop. After the variables have been swapped, we set the parent index to the parent index of the swapped node. 
    void SortUp(T _item)
    {
        int parentindex = (_item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentitem = m_items[parentindex];

            if (_item.CompareTo(parentitem) > 0)
            {
                Swap(_item, parentitem);
            }
            else
            {
                break;
            }

            parentindex = (_item.HeapIndex - 1) / 2;
        }
    }

    /// SWAP
    /// 
    /// This function takes in two elements of T and 
    /// sets the heapindex for each to the other. Then we
    /// create an int and store the heapindex of item a. Then we
    /// set a's heap index to b's and b's is then set to the value we stored.
    /// This swaps their position in the heap. 
    void Swap(T _a, T _b)
    {
        m_items[_a.HeapIndex] = _b;
        m_items[_b.HeapIndex] = _a;
        int itemAIndex = _a.HeapIndex;
        _a.HeapIndex = _b.HeapIndex;
        _b.HeapIndex = itemAIndex;
    }
}

/// IHEAPITEM
/// 
/// This function provides an interface for each item in the heap 
/// for our heapindex and our comparison functions.
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}