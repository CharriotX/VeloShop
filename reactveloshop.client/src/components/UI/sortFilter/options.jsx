export const options = [
    {
        label: 'По алфавиту (возрастание)',
        value: { column: "name", order: "asc" },
    },
    {
        label: 'По алфавиту (убывание)',
        value: { column: "name", order: "desc" },
    },
    {
        label: 'По цене (возрастание)',
        value: { column: "price", order: "asc" },
    },
    {
        label: 'По цене (убывание)',
        value: { column: "price", order: "desc" },
    },
];

export default options;