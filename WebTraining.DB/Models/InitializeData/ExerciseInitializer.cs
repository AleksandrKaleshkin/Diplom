using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebTraining.DB.DataContext;

namespace WebTraining.DB.Models.InitializeData
{
    public class ExerciseInitializer
    {
        WebTrainingContext db;
        public ExerciseInitializer(WebTrainingContext db)
        {
            this.db = db;
        }

        public void InitializeImage()
        {
            db.ImageExercises.AddRange(
                new ImageExercise
                {

                    NameImage = "молоток1.jpg",
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Bitceps/2/молоток1.jpg",
                    ExerciseID = 1,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 1)
                },
                new ImageExercise
                {

                    NameImage = "молоток2.jpg",
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Bitceps/2/молоток2.jpg",
                    ExerciseID = 1,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 1)
                },
                new ImageExercise
                {

                    NameImage = "bitceps.png",
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Muscle/bitceps.png",
                    ExerciseID = 1,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 1)
                },
                new ImageExercise
                {

                    PathImage = "/files/image/ExerciseImage/BaseExercise/Press/1/планка.jpg",
                    NameImage = "планка.jpg",
                    ExerciseID = 2,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 2)
                },
                new ImageExercise
                {

                    PathImage = "/files/image/ExerciseImage/BaseExercise/Muscle/press.png",
                    NameImage = "press.png",
                    ExerciseID = 2,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 2)
                },
                new ImageExercise
                {

                    PathImage= "/files/image/ExerciseImage/BaseExercise/Triceps/1/Отжимание1.jpg",
                    NameImage = "Отжимание1.jpg",
                    ExerciseID = 3,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 3)
                },
                new ImageExercise
                {
                    PathImage= "/files/image/ExerciseImage/BaseExercise/Triceps/1/Отжимание2.jpg",
                    NameImage = "Отжимание2.jpg",
                    ExerciseID = 3,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 3)
                },
                new ImageExercise
                {
                    PathImage= "/files/image/ExerciseImage/BaseExercise/Muscle/triceps.png",
                    NameImage= "triceps.png",
                    ExerciseID = 3,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 3)
                },
                new ImageExercise
                {
                    PathImage= "/files/image/ExerciseImage/BaseExercise/Shoulders/1/жим1.jpg",
                    NameImage = "Армейский жим.jpg",
                    ExerciseID = 4,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 4)
                },
                new ImageExercise
                {
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Shoulders/1/жим2.jpg",
                    NameImage = "Армейский жим.jpg",
                    ExerciseID = 4,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 4)
                },
                new ImageExercise
                {
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Muscle/shoulders.png",
                    NameImage = "shoulders.png",
                    ExerciseID = 4,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 4)
                },
                new ImageExercise
                {
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Legs/1/приседания1.jpg",
                    NameImage = "Приседания со штангой.jpg",
                    ExerciseID = 5,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 5)
                },
                new ImageExercise
                {
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Legs/1/приседания2.jpg",
                    NameImage = "Приседания со штангой.jpg",
                    ExerciseID = 5,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 5)
                },
                new ImageExercise
                {
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Muscle/legs.png",
                    NameImage = "legs.png",
                    ExerciseID = 5,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 5)
                },
                new ImageExercise
                {
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Back/1/Подтягивание1.jpg",
                    NameImage = "Подтягивания на перекладине.jpg",
                    ExerciseID = 6,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 6)
                },
                new ImageExercise
                {
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Back/1/Подтягивание2.jpg",
                    NameImage = "Подтягивания на перекладине.jpg",
                    ExerciseID = 6,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 6)
                },
                new ImageExercise
                {
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Muscle/back.png",
                    NameImage = "back.png",
                    ExerciseID = 6,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 6)
                },
                new ImageExercise
                {
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Breast/2/Отжимания1.jpg",
                    NameImage = "Отжимания от пола.jpg",
                    ExerciseID = 7,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 7)
                },
                new ImageExercise
                {
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Breast/2/Отжимания2.jpg",
                    NameImage = "Отжимания от пола.jpg",
                    ExerciseID = 7,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 7)
                },
                new ImageExercise
                {
                    PathImage = "/files/image/ExerciseImage/BaseExercise/Muscle/breast.png",
                    NameImage = "breast.png",
                    ExerciseID = 7,
                    Exercise = db.Exercises.FirstOrDefault(x => x.ID == 7)
                }
                );
            }

        public void InitializeExercise()
        {
            db.Exercises.AddRange
                    (
                    new Exercise
                    {
                        NameExercise = "Молоток",
                        Description = "Основное упражнение на плечелучевые мышцы с активной проработкой бицепса.\n" +
                        "Техника\n" +
                        "1.Кисти находятся в промежуточной позиции – направлены друг к другу.\n" +
                        "2.В позиции стоя поочередно сгибаем локти,\n" +
                        "3.поднося гантели к плечам.\n" +
                        "4.В нижней точке суставы до конца не разгибаем.",
                        TypeOfMuscleID = 1,
                        TypeOfMuscle = db.TypeOfMuscles.FirstOrDefault(x => x.ID == 1)
                    },
                    new Exercise
                    {
                        NameExercise = "Планка",
                        Description = "Если говорить о статических упражнениях, скорее, именно планка приходит на ум первой. \n" +
                        "Статика – это удержание положения определенный период времени.\n" +
                        "Одни волокна лучше воспринимают статическую нагрузку, а другие – динамическую.\n" +
                        "Оба варианта необходимы для развития крепкого и здорового тела.Для чего же нужна планка ?\n" +
                        "Давайте подробнее рассмотрим виды и технику выполнения упражнения планка.\n" +
                        "Техника выполнения планки на локтях\n" +
                        "Планку на предплечьях выполнять немного проще, поэтому новичкам осваивать упражнение стоит именно с этого варианта.Так же она подходит тем,\n" +
                        "у кого есть травмы и заболевания кистевых суставов.\n" +
                        "Примите положение, стоя на четвереньках,опустите предплечья на пол параллельно друг другу.Локти должны находиться четко под плечевыми суставами.\n" +
                        "Выпрямите ноги и отведите назад, поставив на носки по ширине таза.\n" +
                        "От макушки до пят тело должно удерживаться в прямой линии.\n" +
                        "Важно подтягивать мышцы живота к позвоночнику, делая поясницу плоской, убирая прогиб, а таз подкручивать.\n" +
                        "Дышите свободно без задержки дыхания.",
                        TypeOfMuscleID = 2,
                        TypeOfMuscle = db.TypeOfMuscles.FirstOrDefault(x => x.ID == 2)
                    },
                    new Exercise
                    {
                        NameExercise = "Отжимания на брусьях",
                        Description = "Базовое упражнение служит для укрепления не только грудных мышц, но и трицепсов, и чем меньше расстояние между перекладинами, тем сильнее работают трицепсы.\r\n\r\nРасположив прямые руки на перекладинах, следует повиснуть, согнув колени, но не провисать в плечах.\r\nНа вдохе локти сгибаются под прямым углом.\r\nС выдохом трицепсами выполняются отжимания, возвращаясь в исходное положение.",
                        TypeOfMuscleID = 3,
                        TypeOfMuscle = db.TypeOfMuscles.FirstOrDefault(x => x.ID == 3),                   
                    },
                    new Exercise
                    {
                        NameExercise = "Армейский жим стоя",
                        Description = "Армейский жим является базовым многосуставным упражнением, направленным на проработку дельтовидной мышцы, в большей степени ее переднего пучка. \r\nПридерживаясь основных правил выполнения, можно увеличить и округлить плечи. Главное, не допускать распространенных ошибок.\r\n\r\nОсновные мышцы:\r\n\r\nПередний и средний пучки дельтовидной мышцы.\r\nКлючичная часть большой грудной мышцы.\r\nТрицепсы плеча.\r\nПередние зубчатые мышцы.\r\nНадостная мышца.\r\n\r\nТехника выполнения армейского жима стоя\r\nСтаньте перед штангой, поставьте стопы по ширине таза, обхватите штангу хватом чуть шире плеч.\r\nПоднимитесь с прямой спиной, согните локти и поднимите штангу к верхней части груди (на ключицы).\r\nОпустите локти в пол, не отклоняйтесь корпусом назад, держите позвоночник силой мышц-стабилизаторов.\r\nС выдохом поднимите штангу над головой, полностью разгибая локти.\r\nНа вдохе плавно опустите гриф на ключичную часть груди, не бросая штангу.",
                        TypeOfMuscleID = 4,
                        TypeOfMuscle = db.TypeOfMuscles.FirstOrDefault(x => x.ID == 4)
                    },
                    new Exercise
                    {
                        NameExercise = "Приседания со штангой",
                        Description = "Приседания — одно из самых лучших и ходовых базовых упражнений в бодибилдинге, кроссфите, пауэрлифтинге и у рядовых ЗОЖников.\r\n\r\nНевозможно представить тренировочную программу именно без классических приседаний со штангой. Не верьте тем — кто говорит что приседания со штангой не нужны. Именно выполнение данного упражнения приводит к усиленной секреции главного анаболического гормона у мужчин – тестостерона так же и  гормона роста. Однако  вовсе не означает, что приседания со штангой – это сугубо мужское упражнение. Для девушек и женщины приседы также должны быть одним из основных движений, ведь именно это упражнение лучше других нагружает бедра и ягодицы, придавая им отличную форму.\r\n\r\nТехника выполнения\r\nПодойдите к стойке со штангой\r\nИсходное положение, стоим ровно, штанга на уровне плеч (не выше)\r\nВозьмите гриф, опираясь корпусом так, чтобы со стоек  он находился на трапециях, но не в коем случае не на шейном отделе\r\nСделайте шаг назад чтобы во время движения Вам не мешали стойки\r\nСделайте вдох и начните опускать корпус вниз, при этом движение должно быть до момента, когда бедра окажутся параллельно полу\r\nПрисед до угла 90 градусов зафиксируйте паузу в нижней точке и затем возвращайте корпус на исходную позицию.\r\nВыполнив выдох после 1 повторения приступайте к следующим\r\nЗакончив повторения вернитесь к стойке, закрепите гриф, руками штангу поднимать из за головы крайне не желательно.\r\n\r\nСоветы по выполнению и противопоказания\r\nЖелательно выполнять приседы только тем спортсменам, у кого нет противопоказаний к сгибаниям в суставах:  коленным, голеностопным и тазобедренным, и отсутствуют противопоказаний к осевой нагрузке на позвоночник.\r\nНовичкам не стоит увеличивать вес штанги без поставленной техники и находящегося рядом ассистента или тренера, часто у новичков происходят нарушения техники: такие как увод коленных суставов за стопы, сведение коленей приводящие к коленям «вальгуса»",
                        TypeOfMuscleID = 7,
                        TypeOfMuscle = db.TypeOfMuscles.FirstOrDefault(x => x.ID == 7)
                    },
                    new Exercise
                    {
                        NameExercise = "Подтягивания на перекладине",
                        Description = "Подтягивания — базовое всеми известное популярное упражнение выполняемое с собственным весом тела. Упражнение является многосуставным и отлично прорабатываем много мышечных групп спины и руки. Так же подтягивания укрепляют мышечный корсет и улучшают силовые показатели, укрепляются не только мышцы,  но и связки и сухожилия. Вариантов подтягиваний очень много, существует целый вид спорта — воркаут.\r\nПодтягивания на перекладине техника выполнения\r\nВозьмитесь руками за перекладину удобным Вам хватом: узким, средним, широким или обратным.\r\nСтабилизируйте в висе плечи и лопатки.\r\nВыполняйте подтягивание так чтобы грудная клетка тянулась к перекладине, происходит сгибание локтей.\r\nВ конечной точке нужно чтобы было произведено касание подбородком перекладины.\r\nВозвращайтесь в исходную позицию и продолжайте повторения.\r\n\r\nПротивопоказания по подтягиваниям\r\nЗапрещена компрессионная нагрузка при подтягивания на позвоночник из-за травм: не желательно выполнять упражнение со следующими заболеваниями: сколиоз, артроз, остеохондроз, грыжи и протрузии позвоночника.\r\nНе извивайте и не скругляйте тело в процессе выполнения.\r\nНе рекомендуется задерживать длительно дыхание при выполнении.",
                        TypeOfMuscleID = 8,
                        TypeOfMuscle = db.TypeOfMuscles.FirstOrDefault(x => x.ID == 8)
                    },
                    new Exercise
                    {
                        NameExercise = "Отжимания от пола",
                        Description = "Отжимание от пола, всеми известное популярное упражнение, которое можно выполнять и дома и в зале.\r\nЭто наверно самое первое упражнение для рядового начинающего спортсмена ну и разумеется человека который хочет быть в форме, когда на зал и спорт не хватает времени и сил. Данное упражнение не требует дополнительных трат на инвентарь, для него не нужна поставленная отточенная техника как с жимом штанги лёжа и отжиматься от пола могут абсолютно все — мужчины, женщины, дети и взрослое поколение.\r\n\r\nОтжимания от пола техника\r\n1) Принимаем упор лёжа, ставим ладони шире плеч, поясница прямая, ноги на небольшой расстоянии на носочках.\r\n\r\n2) Вдыхая, опускаем корпус ниже за счёт сгибания локтей.\r\n\r\n3) На выходе, выпрямляем локтевые суставы в исходное положение.\r\n\r\n4) Продолжаем повторение по схеме 1-3.\r\nВ выполнении упражнения основную нагрузку на себя принимают мышцы груди, упражнение уникально тем, чтоб напрягает брюшные мышцы пресса, так же как и планка. Так же укрепляются межреберные мышц, дельты и трёхглавая мышцы плеча.Отжимания укрепляют не только мышцы, но и связки, кости сухожилия, упражнение делает организм крепче.",
                        TypeOfMuscleID = 9,
                        TypeOfMuscle = db.TypeOfMuscles.FirstOrDefault(x => x.ID == 9),
                    }                   
                    );
        }
    }
}

