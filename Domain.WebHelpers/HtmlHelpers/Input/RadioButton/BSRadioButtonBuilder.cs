using System.Collections;
using System.Collections.Generic;

namespace Domain.WebHelpers.HtmlHelpers.Input.RadioButton
{
    public class BSRadioButtonBuilder : IList<BSRadioButton>
    {
        private readonly List<BSRadioButton> _radioButtons = new List<BSRadioButton>();

        public BSRadioButton this[int index]
        {
            get
            {
                return _radioButtons[index];
            }

            set
            {
                _radioButtons[index] = value;
            }
        }
                       
        public int Count
        {
            get
            {
               return  _radioButtons.Count;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public virtual void Add(BSRadioButton item)
        {
            _radioButtons.Add(item);
        } 
        public IBSRadioButton Add()
        {
            BSRadioButton btn = new BSRadioButton();
            _radioButtons.Add(btn);
            return btn;
        }
        public void Clear()
        {
            _radioButtons.Clear();
        }
        public bool Contains(BSRadioButton item)
        {
            return _radioButtons.Contains(item);
        }
        public void CopyTo(BSRadioButton[] array, int arrayIndex)
        {
            _radioButtons.CopyTo(array, arrayIndex);
        }
        public IEnumerator<BSRadioButton> GetEnumerator()
        {
            return _radioButtons.GetEnumerator();
        }
        public int IndexOf(BSRadioButton item)
        {
            return _radioButtons.IndexOf(item);
        }
        public void Insert(int index, BSRadioButton item)
        {
            _radioButtons.Insert(index, item);
        }
        public bool Remove(BSRadioButton item)
        {
           return _radioButtons.Remove(item);
        }
        public void RemoveAt(int index)
        {
            _radioButtons.RemoveAt(index);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _radioButtons.GetEnumerator();
        }

    }
}