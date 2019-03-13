using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BBSObserver.Models.Lecture
{
    public class LectureContextInitializer : DropCreateDatabaseAlways<LectureContext>
    {
        protected override void Seed(LectureContext context)
        {
            LectureContext db = new LectureContext();

            var lectures = new List<Lecture>()
            {
                new Lecture()
                {
                    Name = "プログラミング入門",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = ""
                },

                new Lecture()
                {
                    Name = "情報数学1",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "線形代数1",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "微積分1",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "論理回路",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "プログラミング言語1演習",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = "",
                },
                new Lecture()
                {
                    Name = "プログラミング言語1",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = "",
                },
                new Lecture()
                {
                    Name = "放射線工学基礎論",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "基礎電磁気学",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "情報数学2",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "線形代数2",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "微積分2",
                    ProfessorName = "",
                    Target = Grand.B1,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "電気電子回路論",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "プログラミング言語2",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },
                new Lecture()
                {
                    Name = "計算機システム1",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "情報ネットワーク",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "数値解析",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "情報理論",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "知識工学",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "情報と職業",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "技術英語",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "化学の世界",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "情報工学実験1",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "オートマトンと言語理論",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "データ構造とアルゴリズム",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "計算機システム2",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "オペレーティングシステム",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "数理計画法",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "画像情報工学",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "応用数学1",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "応用解析学",
                    ProfessorName = "",
                    Target = Grand.B2,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "インターンシップ",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "情報工学実験2",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "集積回路工学",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "プログラミング言語3",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "ソフトウェア工学",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "情報システム開発演習",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },
                new Lecture()
                {
                    Name = "データベース論",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "並列分散処理",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "パターン認識",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "技術マネジメント",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "社会資本の設備と運用",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "知的財産権",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "応用数学2",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "統計解析",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "システムデザイン",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "情報工学実験3",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "システム制御工学",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },
                new Lecture()
                {
                    Name = "コンパイラ",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },
                new Lecture()
                {
                    Name = "ビジュアルコンピューティング",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },
                new Lecture()
                {
                    Name = "産業経済論",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },
                new Lecture()
                {
                    Name = "企業理論",
                    ProfessorName = "",
                    Target = Grand.B3,
                    Room = "",
                },

                new Lecture()
                {
                    Name = "卒業論文",
                    ProfessorName = "",
                    Target = Grand.B4,
                    Room = "",
                },
                new Lecture()
                {
                    Name = "ヒューマンコンピュータインタラクション",
                    ProfessorName = "",
                    Target = Grand.B4,
                    Room = "",
                },
            };

            lectures.ForEach(lec => db.Lectures.Add(lec));
            db.SaveChanges();
        }
    }
}