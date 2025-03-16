import { Card, CardContent, Typography, Grid } from '@mui/material';
import { CloudUpload, FolderOpen, Group, Label } from '@mui/icons-material';

export default function Home() {
  return (
    <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', height: '100vh', padding: '16px' }}>
      <Card style={{ width: '100%', maxWidth: '600px', padding: '24px', textAlign: 'center', boxShadow: '0 4px 20px rgba(0,0,0,0.1)', borderRadius: '16px', backgroundColor: '#ffffff' }}>
        <CardContent>
          <Typography variant="h4" style={{ color: '#3f51b5', fontWeight: 'bold' }}>ברוכים הבאים ל-PhotoShare 📷</Typography>
          <Typography variant="body1" style={{ color: '#4a5568', marginTop: '8px' }}>ניהול חכם של התמונות שלך בעזרת טכנולוגיית AI!</Typography>
          
          <Grid container spacing={2} style={{ marginTop: '24px' }}>
            <Grid item xs={12} display="flex" alignItems="center">
              <CloudUpload style={{ color: '#4caf50', fontSize: '40px' }} />
              <Typography style={{ marginLeft: '8px', color: '#4a5568' }}>העלה, ארגן ושתף תמונות בצורה קלה ונוחה</Typography>
            </Grid>
            <Grid item xs={12} display="flex" alignItems="center">
              <FolderOpen style={{ color: '#2196f3', fontSize: '40px' }} />
              <Typography style={{ marginLeft: '8px', color: '#4a5568' }}>ניהול אלבומים מתקדם עם אפשרויות תיוג וחיפוש חכם</Typography>
            </Grid>
            <Grid item xs={12} display="flex" alignItems="center">
              <Group style={{ color: '#f44336', fontSize: '40px' }} />
              <Typography style={{ marginLeft: '8px', color: '#4a5568' }}>זיהוי פנים אוטומטי להקצאת שמות לאנשים בתמונות</Typography>
            </Grid>
            <Grid item xs={12} display="flex" alignItems="center">
              <Label style={{ color: '#ff9800', fontSize: '40px' }} />
              <Typography style={{ marginLeft: '8px', color: '#4a5568' }}>גיבוי מאובטח בענן ושמירה על הרגעים החשובים שלך</Typography>
            </Grid>
          </Grid>
          
          <Typography variant="body1" style={{ marginTop: '24px', fontWeight: 'bold', color: '#4a5568' }}>📂 התחל עכשיו ונהל את הזיכרונות שלך במקום אחד!</Typography>
        </CardContent>
      </Card>
    </div>
  );
}
// import { Card, CardContent, Typography, Grid } from '@mui/material';
// import { CloudUpload, FolderOpen, Group, Label } from '@mui/icons-material';

// export default function Home() {
//   return (
//     <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', height: '100vh', padding: '16px', backgroundColor: '#f0f2f5' }}>
//       <Card style={{ width: '100%', maxWidth: '600px', padding: '24px', textAlign: 'center', boxShadow: '0 4px 20px rgba(0,0,0,0.1)', borderRadius: '16px', backgroundColor: '#ffffff' }}>
//         <CardContent>
//           <Typography variant="h4" style={{ color: '#0078d4', fontWeight: 'bold', fontFamily: 'Segoe UI, sans-serif' }}>ברוכים הבאים ל-PhotoShare 📷</Typography>
//           <Typography variant="body1" style={{ color: '#4a5568', marginTop: '8px', fontFamily: 'Segoe UI, sans-serif' }}>ניהול חכם של התמונות שלך בעזרת טכנולוגיית AI!</Typography>
          
//           <Grid container spacing={2} style={{ marginTop: '24px' }}>
//             <Grid item xs={12} display="flex" alignItems="center">
//               <CloudUpload style={{ color: '#0078d4', fontSize: '40px' }} />
//               <Typography style={{ marginLeft: '8px', color: '#4a5568', fontFamily: 'Segoe UI, sans-serif' }}>העלה, ארגן ושתף תמונות בצורה קלה ונוחה</Typography>
//             </Grid>
//             <Grid item xs={12} display="flex" alignItems="center">
//               <FolderOpen style={{ color: '#0078d4', fontSize: '40px' }} />
//               <Typography style={{ marginLeft: '8px', color: '#4a5568', fontFamily: 'Segoe UI, sans-serif' }}>ניהול אלבומים מתקדם עם אפשרויות תיוג וחיפוש חכם</Typography>
//             </Grid>
//             <Grid item xs={12} display="flex" alignItems="center">
//               <Group style={{ color: '#0078d4', fontSize: '40px' }} />
//               <Typography style={{ marginLeft: '8px', color: '#4a5568', fontFamily: 'Segoe UI, sans-serif' }}>זיהוי פנים אוטומטי להקצאת שמות לאנשים בתמונות</Typography>
//             </Grid>
//             <Grid item xs={12} display="flex" alignItems="center">
//               <Label style={{ color: '#0078d4', fontSize: '40px' }} />
//               <Typography style={{ marginLeft: '8px', color: '#4a5568', fontFamily: 'Segoe UI, sans-serif' }}>גיבוי מאובטח בענן ושמירה על הרגעים החשובים שלך</Typography>
//             </Grid>
//           </Grid>
          
//           <Typography variant="body1" style={{ marginTop: '24px', fontWeight: 'bold', color: '#4a5568', fontFamily: 'Segoe UI, sans-serif' }}>📂 התחל עכשיו ונהל את הזיכרונות שלך במקום אחד!</Typography>
//         </CardContent> 
//       </Card>
//     </div>
//   );
// }
