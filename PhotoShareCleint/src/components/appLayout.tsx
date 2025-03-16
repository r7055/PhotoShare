import { Outlet } from 'react-router-dom';

const AppLayout = () => {
    return (
        <div>
            <header>
                <h1>PhotoShare</h1>
                {/* כאן תוכל להוסיף ניווט */}
            </header>
            <main>
                <Outlet /> {/* כאן ייטען התוכן של כל מסך */}
            </main>
            <footer>
                {/* כאן תוכל להוסיף תוכן עבור פוטר */}
            </footer>
        </div>
    );
};

export default AppLayout;
