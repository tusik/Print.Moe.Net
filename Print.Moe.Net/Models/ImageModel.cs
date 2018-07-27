using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Print.Moe.Net.Models
{
    public class ImageModel
    {
        private long iid;
        private String kid;
        private String author;
        private String tags;
        private String created_at;
        private String source;
        private String score;
        private String md5;
        private String file_size;
        private String file_url;
        private String preview_url;
        private String sample_url;
        private String jpeg_url;
        private int has_children;
        private Double width;
        private Double height;
        private int likes;
        private int status;
        public ImageModel() { }
        public ImageModel(
            long iid,
            string kid,
            string author,
            string tags,
            string created_at,
            string source,
            string score,
            string md5,
            string file_size,
            string file_url,
            string preview_url,
            string sample_url,
            string jpeg_url,
            int has_children,
            double width,
            double height,
            string likes,
            int status)
        {
            this.Author = author;
            this.Iid = iid;
            this.Kid = kid;
            this.Tags = tags;
            this.Created_at = created_at;
            this.Source = source;
            this.Score = score;
            this.Md5 = md5;
            this.File_size = file_size;
            this.File_url = file_url;
            this.Preview_url = preview_url;
            this.Sample_url = sample_url;
            this.Jpeg_url = jpeg_url;
            this.Has_children = has_children;
            this.Width = width;
            this.Height = height;
            if(likes!="")
            this.Likes = Convert.ToInt32(likes);
            this.status = status;
        }
        public ImageModel(
            long iid,
            string kid,
            string author,
            string tags,
            string created_at,
            string source,
            string score,
            string md5,
            string file_size,
            string file_url,
            string preview_url,
            string sample_url,
            string jpeg_url,
            int has_children,
            double width,
            double height)
        {
            this.Author = author;
            this.Iid = iid;
            this.Kid = kid;
            this.Tags = tags;
            this.Created_at = created_at;
            this.Source = source;
            this.Score = score;
            this.Md5 = md5;
            this.File_size = file_size;
            this.File_url = file_url;
            this.Preview_url = preview_url;
            this.Sample_url = sample_url;
            this.Jpeg_url = jpeg_url;
            this.Has_children = has_children;
            this.Width = width;
            this.Height = height;
        }
        public long Iid {
            get{ return iid;}
            set { iid = value; }
        }
        public string Kid {
            get { return kid; }
            set { kid = value; }
        }
        public string Author {
            get { return author; }
            set { author = value; }
        }
        public string Tags {
            get { return tags; }
            set{ tags = value;}
        }
        public string Created_at {
            get { return created_at; }
            set { created_at = value; }
        }
        public string Source {
            get { return source; }
            set { source = value; }
        }
        public string Score
        {
            get { return score; }
            set { score = value;}
        }
        public string Md5 {
            get { return md5; }
            set { md5 = value; }
        }
        public string File_size {
            get { return file_size; }
            set { file_size = value; }
        }
        public string File_url {
            get { return file_url; }
            set { file_url = value; }
        }
        public string Preview_url {
            get { return preview_url; }
            set { preview_url = value; }
        }
        public string Sample_url {
            get { return sample_url; }
            set { sample_url = value; }
        }
        public string Jpeg_url {
            get { return jpeg_url; }
            set { jpeg_url = value; }
        }
        public int Has_children {
            get { return has_children; }
            set { has_children = value; }
        }
        public double Width {
            get { return width; }
            set { width = value; }
        }
        public double Height {
            get { return height; }
            set { height = value; }
        }

        public int Likes
        {
            get { return likes; }
            set { likes = value; }
        }

        public int Status {
            get { return status; }
            set { status = value; }
        }
    }
}