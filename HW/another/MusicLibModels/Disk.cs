using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MusicLibModels
{
    public class Disk : IValidatableObject, INotifyPropertyChanged
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
        private int _authorId;
        public int AuthorId
        {
            get { return _authorId; }
            set
            {
                if (!_authorId.Equals(value))
                {
                    _authorId = value;
                    OnPropertyChanged(nameof(AuthorId));
                }
            }
        }

        [Required]
        private Author _author;
        public Author Author
        {
            get { return _author; }
            set
            {
                if (!_author?.Equals(value) ?? value != null)
                {
                    _author = value;
                    OnPropertyChanged(nameof(Author));
                }
            }
        }

        [Required]
        [DataType(DataType.Date)]
        private DateTime _publishDate;
        public DateTime PublishDate
        {
            get { return _publishDate; }
            set
            {
                if (!_publishDate.Equals(value))
                {
                    _publishDate = value;
                    OnPropertyChanged(nameof(PublishDate));
                }
            }
        }

        [Required]
        private ObservableCollection<Genre> _genres;
        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set
            {
                if (!_genres?.Equals(value) ?? value != null)
                {
                    _genres = value;
                    OnPropertyChanged(nameof(Genres));
                }
            }
        }

        [Required]
        private ObservableCollection<Song> _songs;
        public ObservableCollection<Song> Songs
        {
            get { return _songs; }
            set
            {
                if (!_songs?.Equals(value) ?? value != null)
                {
                    _songs = value;
                    OnPropertyChanged(nameof(Songs));
                }
            }
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Id == default)
                errors.Add(new ValidationResult("Disk identifier is default(empty)."));
            if (string.IsNullOrWhiteSpace(this.Name) || Name.Length < 3 || Name.Length > 50)
                errors.Add(new ValidationResult("Wrong Disk Name."));
            if (AuthorId == default)
                errors.Add(new ValidationResult("Disk's AuthorId is default(empty)."));
            if (Author == null)
                errors.Add(new ValidationResult("Disk's Author points to null."));
            if (PublishDate == default)
                errors.Add(new ValidationResult("Disk's Publish Date is default(empty)."));
            if (Genres == null || Genres.Count == 0)
                errors.Add(new ValidationResult("Disk doesn't have any Genres."));
            if (Songs == null || Songs.Count == 0)
                errors.Add(new ValidationResult("Disk doesn't have any songs."));

            return errors;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return $"Id:{Id},Name:{Name},Author:[{Author.ToString()}],Publish Date:{PublishDate}," +
                $"Genres:[{Genres.Select(g => g.ToString()).Aggregate((f, s) => f + ',' + s)}]," +
                $"Songs:[{Songs.Select(s => s.ToString()).Aggregate((f, s) => f + ',' + s)}]";
        }
        public override bool Equals(object obj)
        {
            return Name.Equals((obj as Disk).Name) && Author.Equals((obj as Disk).Author) &&
                PublishDate.Equals((obj as Disk).PublishDate) &&
                Genres.All(g => (obj as Disk).Genres.Contains(g)) &&
                Songs.All(s => (obj as Disk).Songs.Contains(s));
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() + Author.GetHashCode() + PublishDate.GetHashCode() + Genres.Sum(g => g.GetHashCode()) + Songs.Sum(s => s.GetHashCode());
        }
    }
}
