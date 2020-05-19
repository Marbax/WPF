using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLibModels
{
    public class Author
    {
        [ForeignKey(nameof(Disk))]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Id == default)
                errors.Add(new ValidationResult("Author identifier is default."));
            if (string.IsNullOrWhiteSpace(this.Name) || Name.Length < 3 || Name.Length > 50)
                errors.Add(new ValidationResult("Wrong Author Name."));

            return errors;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return $"Id:{Id},Name:{Name}";
        }
        public override bool Equals(object obj)
        {
            return Name.Equals((obj as Author).Name);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
