import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';

interface Props {
    onChangeFechaPermiso: (date: any) => void;
    label:string;
}

const DatePickerModel = ({onChangeFechaPermiso, label}:Props) => {

    return (
        <>
        <LocalizationProvider dateAdapter={AdapterDayjs}>
            <DemoContainer components={['DateTimePicker']}>
            <DateTimePicker label={label} onChange={(e)=>onChangeFechaPermiso(e)}/>
            </DemoContainer>
        </LocalizationProvider> 
        </>
    )
}

export default DatePickerModel;