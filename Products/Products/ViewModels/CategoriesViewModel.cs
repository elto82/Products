using Products.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.ViewModels
{
    public class CategoriesViewModel
    {
        #region Properties

        public ObservableCollection<Category> Categories { get; set; }
        #endregion
        #region Contructrs
        public CategoriesViewModel()
        {
            LoadCategories();
        }

        #endregion

        #region Methods
        private void LoadCategories()
        {
            throw new NotImplementedException();
        }
        #endregion

    }

}
