import React from 'react';
import { MenuItem, Select} from '@mui/material';

interface Option {
  value: string | number;
  label: string;
}

interface Props {
  label: string;
  options: Option[];
  value: string | number;
  onChange: (value: string | number) => void;
  fullWidth?: boolean;
}

const MuiSelect: React.FC<Props> = ({
  label,
  options,
  value,
  onChange,
}) => {
  const handleChange = (event: any) => {
    console.log('handleChange', event.target.value);
    const val = isNaN(Number(event.target.value))
      ? event.target.value
      : Number(event.target.value);
    onChange(val);
  };

  return (

      <Select
        labelId={`${label}-label`}
        id={`${label}-select`}
        value={value}
        label={label}
        onChange={handleChange}
        style={{ width: "200px"}}
      >
        {options.map((opt) => (
          <MenuItem key={opt.value} value={opt.value}>
            {opt.label}
          </MenuItem>
        ))}
      </Select>

  );
};

export default MuiSelect;
