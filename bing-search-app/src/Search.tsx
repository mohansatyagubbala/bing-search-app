import React, { useState } from "react";
import axios from "axios";

const Search: React.FC = () => {
    const [query, setQuery] = useState<string>("");
    const [results, setResults] = useState<any[]>([]);

    const search = async () => {
        const apiUrl = `https://api.bing.microsoft.com/v7.0/search?q=${query}`;
        const headers = {
            "Ocp-Apim-Subscription-Key": process.env.REACT_APP_BING_API_KEY!,
        };

        try {
            const response = await axios.get(apiUrl, { headers });
            setResults(response.data?.webPages?.value);
        } catch (error) {
            console.error("Error fetching data:", error);
        }
    };

    return (
        <>
            <div className="search-container">
                <input
                    type="text"
                    value={query}
                    onChange={(e) => setQuery(e.target.value)}
                    placeholder="Search for something..."
                />
                <button className="search-button" onClick={search}>Search</button>

            </div>
            <ul className="search-results">
                {results.map((result) => (
                    <li key={result.id}>
                        <a href={result.url}>{result.name}</a>
                    </li>
                ))}
            </ul>
        </>
    );
};

export default Search;