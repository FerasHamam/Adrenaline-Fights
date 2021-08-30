{
  "nbformat": 4,
  "nbformat_minor": 0,
  "metadata": {
    "colab": {
      "name": "IR.ipynb",
      "provenance": [],
      "collapsed_sections": [],
      "authorship_tag": "ABX9TyPl8igB58EPVKY5+t0jwP0k",
      "include_colab_link": true
    },
    "kernelspec": {
      "display_name": "Python 3",
      "name": "python3"
    },
    "language_info": {
      "name": "python"
    }
  },
  "cells": [
    {
      "cell_type": "markdown",
      "metadata": {
        "id": "view-in-github",
        "colab_type": "text"
      },
      "source": [
        "<a href=\"https://colab.research.google.com/github/FerasHamam/Adrenaline-Fights/blob/main/IR.py\" target=\"_parent\"><img src=\"https://colab.research.google.com/assets/colab-badge.svg\" alt=\"Open In Colab\"/></a>"
      ]
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "Nhr_AOuD9uEG",
        "colab": {
          "base_uri": "https://localhost:8080/"
        },
        "outputId": "44ffeb8d-9e55-45aa-9395-827295a3138a"
      },
      "source": [
        "import json\n",
        "\n",
        "import pandas as pd\n",
        "\n",
        "import re\n",
        "\n",
        "import string\n",
        "\n",
        "import math\n",
        "\n",
        "from collections import Counter\n",
        "\n",
        "from operator import itemgetter\n",
        "\n",
        "import nltk\n",
        "nltk.download('stopwords')\n",
        "nltk.download('punkt')\n",
        "from nltk import PorterStemmer\n",
        "from nltk import word_tokenize\n",
        "from nltk.corpus import stopwords\n",
        "\n",
        "import numpy as np\n"
      ],
      "execution_count": null,
      "outputs": [
        {
          "output_type": "stream",
          "text": [
            "[nltk_data] Downloading package stopwords to /root/nltk_data...\n",
            "[nltk_data]   Package stopwords is already up-to-date!\n",
            "[nltk_data] Downloading package punkt to /root/nltk_data...\n",
            "[nltk_data]   Package punkt is already up-to-date!\n"
          ],
          "name": "stdout"
        }
      ]
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "-DI3C8WXNn6N",
        "colab": {
          "base_uri": "https://localhost:8080/"
        },
        "outputId": "fa421b50-41ab-4e41-fe61-7f78e43ea94c"
      },
      "source": [
        "from google.colab import drive\n",
        "drive.mount('/content/gdrive')\n",
        "df = pd.read_json('gdrive/MyDrive/cranfield_data.json').T"
      ],
      "execution_count": null,
      "outputs": [
        {
          "output_type": "stream",
          "text": [
            "Drive already mounted at /content/gdrive; to attempt to forcibly remount, call drive.mount(\"/content/gdrive\", force_remount=True).\n"
          ],
          "name": "stdout"
        }
      ]
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "--dccFI3-NmM"
      },
      "source": [
        "#functions\n",
        "stemming = PorterStemmer()\n",
        "noOfTokensbefore = 0\n",
        "stopEn = stopwords.words('english')\n",
        "def TextCleaning(data):\n",
        "    global noOfTokensbefore\n",
        "    global stopEn \n",
        "    #remove stopwords and stemming\n",
        "    data = word_tokenize(data) #tokeniztion\n",
        "    noOfTokensbefore += len(data)\n",
        "    data = ' '.join([stemming.stem(word) for word in data if word not in stopEn ])\n",
        "    data = re.sub(\"[^\\w\\s]\",\"\",data) #remove punctuation\n",
        "    data = re.sub(\"\\d+\",\"\",data) #remove numbers\n",
        "    data = re.sub(r'\\b\\w{1,2}\\b', '', data) #remove words with length of 1 or 2 characters\n",
        "    data = word_tokenize(data) #tokeniztion\n",
        "    return data\n",
        "\n",
        "#partA\n",
        "\n",
        "for row in df:\n",
        "  df[row]['author'] = TextCleaning(df[row]['author'])\n",
        "  df[row]['bibliography'] = TextCleaning(df[row]['bibliography'])\n",
        "  df[row]['body'] = TextCleaning(df[row]['body'])\n",
        "  df[row]['title'] = TextCleaning(df[row]['title'])"
      ],
      "execution_count": null,
      "outputs": []
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "XZ7p9WBHQwoS",
        "colab": {
          "base_uri": "https://localhost:8080/"
        },
        "outputId": "4e8e8a1e-c933-4000-e14c-1c60c65e7dd7"
      },
      "source": [
        "#print part!!\n",
        "df = df.T\n",
        "#unique words(1+2)\n",
        "mergedDoc = df['author'] + df['bibliography'] +df['body']+ df['title']\n",
        "wordsUnique = []\n",
        "for sent in mergedDoc:\n",
        "  wordsUnique += sent\n",
        "\n",
        "#the 30 most frequent words and Words Once\n",
        "mergedDocCombined = Counter(wordsUnique).most_common()\n",
        "\n",
        "NoWordsOnce = 0 #counter for wordes that occured once!(3)\n",
        "for key in mergedDocCombined:\n",
        "  if key[1] == 1:\n",
        "    NoWordsOnce +=1\n",
        "\n",
        "mostFreq = [] #list to hold the 30 most freq words(4)\n",
        "counter = 0\n",
        "for key in mergedDocCombined:\n",
        "  if(counter < 30):#gets the first 30 words\n",
        "    mostFreq.append(key)\n",
        "    counter +=1\n",
        "  else:\n",
        "    break\n",
        "\n",
        "print('1-')\n",
        "print(f\"number of tokens before text cleaning : {noOfTokensbefore}\")\n",
        "print(f\"number of tokens after text cleaning : {len(wordsUnique)}\")\n",
        "print(f\"\\n2- number of Unique words : {len(set(wordsUnique))}\")\n",
        "print(f\"\\n3- number of words that occur only once : {NoWordsOnce}\")\n",
        "print(f\"\\n4- the 30 most frequent words in the Cranfield collection: {mostFreq}\")\n",
        "print(f\"\\n5- The average number of word tokens per document : {round(len(wordsUnique)/1400)}\")"
      ],
      "execution_count": null,
      "outputs": [
        {
          "output_type": "stream",
          "text": [
            "1-\n",
            "number of tokens before text cleaning : 276398\n",
            "number of tokens after text cleaning : 138228\n",
            "\n",
            "2- number of Unique words : 6789\n",
            "\n",
            "3- number of words that occur only once : 2859\n",
            "\n",
            "4- the 30 most frequent words in the Cranfield collection: [('flow', 2332), ('number', 1439), ('pressur', 1436), ('effect', 1136), ('boundari', 1102), ('result', 1100), ('layer', 1036), ('theori', 957), ('method', 948), ('solut', 908), ('mach', 906), ('bodi', 848), ('wing', 831), ('equat', 822), ('heat', 820), ('use', 761), ('surfac', 714), ('distribut', 708), ('present', 700), ('shock', 679), ('obtain', 649), ('problem', 636), ('temperatur', 626), ('superson', 624), ('ratio', 615), ('investig', 606), ('calcul', 605), ('approxim', 600), ('plate', 586), ('veloc', 575)]\n",
            "\n",
            "5- The average number of word tokens per document : 99\n"
          ],
          "name": "stdout"
        }
      ]
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "s7YMWPBA0VZ2"
      },
      "source": [
        "#fucntion for tf & df\n",
        "postingListForDf = dict()\n",
        "postingListForTf = dict()\n",
        "def prepareDTF(docs):\n",
        "  global postingListForDf\n",
        "  global postingListForTf\n",
        "  idDoc = 1\n",
        "  for sent in docs:\n",
        "    for word in sent:\n",
        "      postingListForDf.setdefault(word, set()).add(idDoc)\n",
        "      postingListForTf.setdefault(word, []).append(idDoc)\n",
        "    idDoc += 1\n",
        "\n",
        "#partB to apply the function above\n",
        "prepareDTF(mergedDoc)\n",
        "postingListDF = []\n",
        "postingListTF = []\n",
        "\n",
        "#calculates DF\n",
        "for key in postingListForDf:\n",
        "  docF = len(postingListForDf[key])\n",
        "  postingListDF.append((key,docF,postingListForDf[key]))\n",
        "postingListDF = sorted(postingListDF,key = itemgetter(0))\n",
        "\n",
        "#calculates TF\n",
        "docDict = dict() #holds the initial shape of postingList for TF\n",
        "for key in postingListForTf:\n",
        "  for doc in postingListForTf[key]:\n",
        "      docDict.setdefault(doc,[]).append(key)\n",
        "\n",
        "for doc in docDict:\n",
        "  counter = {}\n",
        "  counter = Counter(docDict[doc])\n",
        "  for word in counter:\n",
        "    counter[word] = counter[word] /len(docDict[doc])#tf = tfWord/len of doc\n",
        "  postingListTF.append((doc,counter))\n",
        "postingListTF = sorted(postingListTF, key = itemgetter(0))\n"
      ],
      "execution_count": null,
      "outputs": []
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "OY1iyCd2276i"
      },
      "source": [
        "#postingList idf\n",
        "\n",
        "postingListIDF = dict()\n",
        "for key in postingListForDf:\n",
        "  IdocF = math.log(1400/len(postingListForDf[key]))\n",
        "  postingListIDF.setdefault(key,(IdocF,postingListForDf[key]))"
      ],
      "execution_count": null,
      "outputs": []
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "0c4gji72Cjvb"
      },
      "source": [
        "Queries = open('gdrive/MyDrive/Queries.txt','r')\n",
        "Query = Queries.readlines()\n",
        "#Query Cleaning\n",
        "index = 0\n",
        "for sent in Query:\n",
        "  Query[index] = TextCleaning(sent)\n",
        "  index += 1\n",
        "\n",
        "QueryTf = dict()\n",
        "\n",
        "#prepare df and tf for Queries\n",
        "idQ = 1;\n",
        "for sent in Query:\n",
        "  for word in sent:\n",
        "    QueryTf.setdefault(word, []).append(idQ)\n",
        "  idQ += 1\n",
        "\n",
        "#tf \n",
        "#calculates TF\n",
        "DocDict = dict() #holds the initial shape of postingList for TF\n",
        "for key in QueryTf:\n",
        "  for doc in QueryTf[key]:\n",
        "      DocDict.setdefault(doc,[]).append(key)\n",
        "\n",
        "QueryTf = []\n",
        "for doc in DocDict:\n",
        "  counter = {}\n",
        "  counter = Counter(DocDict[doc])\n",
        "  for word in counter:\n",
        "    counter[word] = counter[word] /len(docDict[doc])#tf = tfWord/len of doc\n",
        "  QueryTf.append((doc,counter))\n",
        "QueryTf = sorted(QueryTf, key = itemgetter(0))\n"
      ],
      "execution_count": null,
      "outputs": []
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "qiCRXqwdI5fT"
      },
      "source": [
        "#vector space retrieval model tf*idf for docs\n",
        "doc_tf_idf = dict()\n",
        "\n",
        "for doc in postingListTF:\n",
        "  values = dict()\n",
        "  for word in postingListIDF:\n",
        "    if doc[0] in postingListIDF[word][1]:\n",
        "      tf = doc[1][word]\n",
        "      idf = postingListIDF[word][0]\n",
        "      values.setdefault(word,idf*math.log(1+tf))\n",
        "    doc_tf_idf.setdefault(doc[0], values)\n",
        "\n",
        "# vector space retrieval model tf*idf for queries\n",
        "# method1\n",
        "Query_tf_idf= dict()\n",
        "for Quer in QueryTf:\n",
        "  values = dict()\n",
        "  for word in postingListIDF:\n",
        "    if word in Quer[1] :\n",
        "      tf = Quer[1][word]\n",
        "      idf = postingListIDF[word][0]\n",
        "      values.setdefault(word,math.log(1+tf)*idf)\n",
        "  Query_tf_idf.setdefault(Quer[0],values)\n",
        "\n",
        "## method2\n",
        "# Query_tf_idf = dict()\n",
        "# for Quer in QueryTf:\n",
        "#   for word in postingListIDF:\n",
        "#     if word in Quer[1] :\n",
        "#       tf = Quer[1][word]\n",
        "#       idf = postingListIDF[word][0]\n",
        "#       Query_tf_idf.setdefault(Quer[0],[]).append((word,idf*tf,postingListForDf[word]))"
      ],
      "execution_count": null,
      "outputs": []
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "TpLBpzpYWlR3"
      },
      "source": [
        "#normalize the length of the docs\n",
        "docsNorm = dict()\n",
        "for doc in doc_tf_idf:\n",
        "  sum = 0\n",
        "  for word in doc_tf_idf[doc]:\n",
        "    sum += doc_tf_idf[doc][word]**2\n",
        "  docsNorm[doc] = math.sqrt(sum)"
      ],
      "execution_count": null,
      "outputs": []
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "JO6rwo2jCOvi"
      },
      "source": [
        "# #cosine sim\n",
        "\n",
        "# cos = dict()\n",
        "# Ranked = dict()\n",
        "# for query in Query_tf_idf:\n",
        "#   for doc in doc_tf_idf:\n",
        "#     for word in Query_tf_idf[query]:\n",
        "#       if word[0] in doc_tf_idf[doc]:\n",
        "#         docTfIdf = doc_tf_idf[doc][word[0]]\n",
        "#         queryTfIdf = word[1]\n",
        "#         if doc in cos:\n",
        "#           cos[doc] += (queryTfIdf*docTfIdf)\n",
        "#         else:\n",
        "#           cos[doc] = queryTfIdf*docTfIdf\n",
        "#   Ranked[query] = cos\n",
        "#   cos = dict()\n",
        "# for query in Ranked:\n",
        "#   for doc in Ranked[query]:\n",
        "#     Ranked[query][doc] /= docsNorm[doc]\n",
        "#   Ranked[query] = sorted(Ranked[query].items(), key = itemgetter(1),reverse=True)"
      ],
      "execution_count": null,
      "outputs": []
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "VEXQjDJNd2vf"
      },
      "source": [
        "#vector space model\n",
        "#to find the index of words\n",
        "wordsUnique = set(wordsUnique)\n",
        "wordIndex = dict()\n",
        "index=0\n",
        "for word in wordsUnique:\n",
        "  wordIndex.setdefault(word,index)\n",
        "  index+=1\n",
        "\n",
        "documents = dict()\n",
        "ind = 1\n",
        "for doc in mergedDoc:\n",
        "  if not doc:\n",
        "    ind+=1\n",
        "    continue\n",
        "  documents.setdefault(ind,np.zeros(len(wordsUnique)))\n",
        "  for word in doc:\n",
        "     documents[ind][wordIndex[word]] = doc_tf_idf[ind][word]\n",
        "  ind += 1\n",
        "\n",
        "\n",
        "queries = dict()\n",
        "for query in Query_tf_idf:\n",
        "  queries.setdefault(query,np.zeros(len(wordsUnique)))\n",
        "  for word in Query_tf_idf[query]:\n",
        "    queries[query][wordIndex[word]] = Query_tf_idf[query][word]\n",
        "\n",
        "# end of vector space model\n",
        "\n",
        "#cos\n",
        "cos = dict()  \n",
        "for q in queries:\n",
        "  cosSim = dict()\n",
        "  for d in documents:\n",
        "    cosSim.setdefault(d,np.dot(queries[q],documents[d])/docsNorm[d])\n",
        "  cos.setdefault(q,cosSim)\n",
        "\n",
        "#rank by descending order\n",
        "for query in cos:\n",
        "  cos[query] = sorted(cos[query].items(), key = itemgetter(1),reverse=True)"
      ],
      "execution_count": null,
      "outputs": []
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "g2cTPpP2x77v"
      },
      "source": [
        "smallRelevance = open('gdrive/MyDrive/smallRelevance.txt','r')\n",
        "Relevances = smallRelevance.readlines()\n",
        "\n",
        "tp = dict()\n",
        "for q in Relevances:\n",
        "  queryDoc = word_tokenize(q)\n",
        "  tp.setdefault(int(queryDoc[0]),[]).append(queryDoc[1])"
      ],
      "execution_count": null,
      "outputs": []
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "vOr2Fn-U1U-u"
      },
      "source": [
        "#Average\n",
        "\n",
        "def calculateInfo(iters, query):\n",
        "  j = 0\n",
        "  global cos\n",
        "  global tp\n",
        "  global accuracy\n",
        "  global precision\n",
        "  global recall\n",
        "  global f1\n",
        "  trueP = 0\n",
        "  trueN = len(cos[query]) - iters\n",
        "  falseP = 0\n",
        "  falseN = len(tp[query])\n",
        "  while j<iters:\n",
        "    if str(cos[query][j][0]) in tp[query]:\n",
        "      trueP += 1\n",
        "      falseN -= 1\n",
        "    else:\n",
        "      falseP += 1\n",
        "      trueN -= 1\n",
        "    j+=1\n",
        "  accuracy += (trueP+trueN)/(trueP+trueN+falseP+falseN)\n",
        "  precision += trueP/(trueP+falseP)\n",
        "  recall = trueP/(trueP+falseN)\n",
        "  f1 += trueP/(trueP+0.5*(falseP+falseN))"
      ],
      "execution_count": null,
      "outputs": []
    },
    {
      "cell_type": "code",
      "metadata": {
        "id": "W9MScA0v94Hr",
        "colab": {
          "base_uri": "https://localhost:8080/"
        },
        "outputId": "4a8a1bc5-0590-4c24-acda-c6855c088508"
      },
      "source": [
        "accuracy = 0\n",
        "precision = 0\n",
        "recall = 0\n",
        "f1 = 0\n",
        "for q in cos:\n",
        "  calculateInfo(10,q)\n",
        "print(f'Accuracy : {accuracy/q}')\n",
        "print(f'Precision : {precision/q}')\n",
        "print(f'Recall : {recall/q}')\n",
        "print(f'f1: {f1/q}')"
      ],
      "execution_count": null,
      "outputs": [
        {
          "output_type": "stream",
          "text": [
            "Accuracy : 0.9863892826700269\n",
            "Precision : 0.2\n",
            "Recall : 0.008333333333333333\n",
            "f1: 0.17447510256371151\n"
          ],
          "name": "stdout"
        }
      ]
    },
    {
      "cell_type": "code",
      "metadata": {
        "colab": {
          "base_uri": "https://localhost:8080/"
        },
        "id": "sD9DR5v2APeh",
        "outputId": "a48d1ff1-e1f2-4312-ef13-02ea6e31523f"
      },
      "source": [
        "accuracy = 0\n",
        "precision = 0\n",
        "recall = 0\n",
        "f1 = 0\n",
        "for q in cos:\n",
        "  calculateInfo(50,q)\n",
        "print(f'Accuracy : {accuracy/q}')\n",
        "print(f'Precision : {precision/q}')\n",
        "print(f'Recall : {recall/q}')\n",
        "print(f'f1: {f1/q}')"
      ],
      "execution_count": null,
      "outputs": [
        {
          "output_type": "stream",
          "text": [
            "Accuracy : 0.9621774973151652\n",
            "Precision : 0.11600000000000002\n",
            "Recall : 0.016666666666666666\n",
            "f1: 0.18029943881574895\n"
          ],
          "name": "stdout"
        }
      ]
    },
    {
      "cell_type": "code",
      "metadata": {
        "colab": {
          "base_uri": "https://localhost:8080/"
        },
        "id": "OHUUFuWWAPwg",
        "outputId": "e1d48342-49ff-4e88-9410-a68115dffbb7"
      },
      "source": [
        "accuracy = 0\n",
        "precision = 0\n",
        "recall = 0\n",
        "f1 = 0\n",
        "for q in cos:\n",
        "  calculateInfo(100,q)\n",
        "print(f'Accuracy : {accuracy/q}')\n",
        "print(f'Precision : {precision/q}')\n",
        "print(f'Recall : {recall/q}')\n",
        "print(f'f1: {f1/q}')"
      ],
      "execution_count": null,
      "outputs": [
        {
          "output_type": "stream",
          "text": [
            "Accuracy : 0.9245753764085407\n",
            "Precision : 0.071\n",
            "Recall : 0.016666666666666666\n",
            "f1: 0.12345576137962089\n"
          ],
          "name": "stdout"
        }
      ]
    }
  ]
}