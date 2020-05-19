using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicLibModels
{
    public class Song : IValidatableObject, INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (!_id.Equals(value))
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        [Required]
        [StringLength(50), MinLength(3)]
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (!_name?.Equals(value) ?? value != null)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        private float _durability;
        public float Durability
        {
            get { return _durability; }
            set
            {
                if (!_durability.Equals(value))
                {
                    _durability = value;
                    OnPropertyChanged(nameof(Durability));
                }
            }
        }

        private ObservableCollection<Disk> _disks;
        public virtual ObservableCollection<Disk> Disks
        {
            get { return _disks; }
            set
            {
                if (!_disks?.Equals(value) ?? value != null)
                {
                    _disks = value;
                    OnPropertyChanged(nameof(Disks));
                }
            }
        }

        /// <summary>
        /// Self validation
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Id == default)
                errors.Add(new ValidationResult("Song identifier is default."));
            if (string.IsNullOrWhiteSpace(this.Name))
                errors.Add(new ValidationResult("Wrong song Name."));
            if (Durability < 1)
                errors.Add(new ValidationResult("Song durability to low."));
            //            if (Disks == null)
            //                errors.Add(new ValidationResult("Song doesn't reference to any disks."));

            return errors;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return $"Id:{Id},Name:{Name},Durability:{Durability}";
        }
        public override bool Equals(object obj)
        {
            return Name.Equals((obj as Song).Name) && Durability.Equals((obj as Song).Durability);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() + Durability.GetHashCode();
        }

    }
}
