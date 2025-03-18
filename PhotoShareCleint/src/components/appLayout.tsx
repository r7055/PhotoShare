import { AppBar, Toolbar, Typography } from '@mui/material';
import { Outlet } from 'react-router-dom';

const AppLayout = () => {
    return (
        <div>
        <AppBar position="static">
            <Toolbar>
                <Typography variant="h4" component="div" sx={{ flexGrow: 1 }}>
                    PhotoShare
                </Typography>
                <Typography variant="subtitle1">
                    ניהול תמונות חכם באמצעות AI - ארגון, תיוג, שיתוף וזיהוי פנים
                </Typography>
            </Toolbar>
        </AppBar>

            <main>
                <Outlet /> {/* כאן ייטען התוכן של כל מסך */}
            </main>
            <AppBar position="static" color="primary" sx={{ top: 'auto', bottom: 0 }}>
            <Toolbar>
                <Typography variant="body1" color="inherit" sx={{ flexGrow: 1 }}>
                    © 2023 PhotoShare. כל הזכויות שמורות.
                </Typography>
                <Typography variant="body2" color="inherit">
                    <a href="/privacy-policy" style={{ color: 'inherit', textDecoration: 'none' }}>מדיניות פרטיות</a>
                </Typography>
            </Toolbar>
        </AppBar>
        </div>
    );
};

export default AppLayout;
