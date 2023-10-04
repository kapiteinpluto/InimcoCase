<template>
  <h2 class="pageTitle">User details</h2>
  <div class="infobox col">
    <div class="row basicInfo">
      <img
        class="profilePicture"
        src="@/assets/profile.png"
        alt="profile picture"
      />
      <div class="namebox">
        <InputField v-model="firstName" inputName="firstname"></InputField>
        <InputField v-model="lastName" inputName="lastname"></InputField>
      </div>
    </div>
    <div class="row detailInfo">
      <div class="col">
        <h3>Social skills</h3>
        <div v-for="(skill, index) in skills" :key="index">
          <input type="text" v-model="skills[index]" />
          <button class="remove" @click="skills.splice(index, 1)">-</button>
        </div>
        <button class="add" @click="this.skills.push('')">Add</button>
      </div>
      <div class="col">
        <h3>Socialmedia</h3>
        <div
          class="socialmediaInputs"
          v-for="(account, index) in socialMedia"
          :key="index"
        >
          <select v-model="account.type">
            <option
              v-for="option in ['facebook', 'twitter', 'linkedin', 'other']"
              :key="option"
            >
              {{ option }}
            </option>
          </select>
          <input type="text" v-model="account.address" />
          <button class="remove" @click="this.socialMedia.splice(index, 1)">
            -
          </button>
        </div>
        <button
          class="add"
          @click="this.socialMedia.push({ type: '', address: '' })"
        >
          Add
        </button>
      </div>
    </div>
    <a @click="setDetails()" class="saveButton">Save</a>
  </div>
  <div class="feedbackbox">
    <p>The number of vowels: {{ vowelCount }}</p>
    <p>The number of constenants: {{ constenantCount }}</p>
    <p>The reverse full name: {{ reverseName }}</p>
    <p>JSON:</p>
    <pre class="jsonView">{{ json }}</pre>
  </div>
</template>

<script>
import axios from "axios";
import InputField from "../atoms/InputField.vue";

export default {
  components: { InputField },
  data() {
    return {
      id: 1,
      firstName: "",
      lastName: "",
      skills: [],
      socialMedia: [],
      errors: [],
    };
  },
  created() {
    this.getDetails();
  },
  computed: {
    vowelCount() {
      const vowels = ["a", "e", "i", "o", "u"];
      let count = 0;
      (this.firstName + this.lastName)
        .toLowerCase()
        .split("")
        .forEach((letter) => {
          if (vowels.includes(letter)) {
            count++;
          }
        });
      return count;
    },
    constenantCount() {
      return (this.firstName + this.lastName).length - this.vowelCount;
    },
    reverseName() {
      return (this.firstName + " " + this.lastName)
        .split("")
        .reverse()
        .join("");
    },
    json() {
      return JSON.stringify(this.$data, null, 2);
    },
  },
  methods: {
    getDetails() {
      const url = "http://localhost:5236/user/" + this.id;
      axios.get(url).then((res) => {
        this.id = res.data.id;
        this.firstName = res.data.firstName;
        this.lastName = res.data.lastName;
        this.skills = res.data.skills;
        this.socialMedia = res.data.socialMedia;
      });
    },
    setDetails() {
      if (this.errors.length == 0) {
        const url = "http://localhost:5236/user/" + this.id;
        axios
          .put(url, {
            id: this.id,
            firstName: this.firstName,
            lastName: this.lastName,
            skills: this.skills,
            socialMedia: this.socialMedia,
          })
          .then(() => {
            console.log("saved data");
          });
      } else {
        console.log("resolve errors before saving");
      }
    },
    validateText() {
      const validCharacters = /^[A-Za-z]+$/;
      if (!this.name) {
        this.nameError = "is required.";
      } else if (this.name.length < 2 || this.name.length > 50) {
        this.nameError = "must be between 2 and 50 characters.";
      } else if (!this.name.match(validCharacters)) {
        this.nameError = "can only contain alphabetical characters.";
      } else {
        this.nameError = "";
      }
    },
    triggerError(error) {
      let index = this.errors.findIndex((x) => x == error);
      if (index == -1) this.errors.push(error);
    },
    removeError(error) {
      let index = this.errors.findIndex((x) => x == error);
      this.errors.splice(index, 1);
    },
  },
};
</script>

<style lang="scss" scoped>
.col {
  display: flex;
  flex-direction: column;
}
.row {
  display: flex;
  flex-direction: row;
}

.pageTitle {
  font-size: 2rem;
}

.infobox {
  border: 0.1rem solid rgb(0, 0, 0);
  border-radius: 2rem;
  width: 90%;
  max-width: 80rem;
  margin: auto;
  background: rgb(220, 220, 220);

  .basicInfo {
    display: flex;
    justify-content: center;
    flex-wrap: wrap;
    .profilePicture {
      width: 10rem;
      padding: 0.5rem 2rem;
      background: white;
      border-radius: 1rem 0 0 1rem;
    }
    .namebox {
      display: flex;
      flex-direction: column;
      justify-content: space-evenly;
      background: white;
      padding-right: 1rem;
      border-radius: 0 1rem 1rem 0;
    }
  }
  .detailInfo {
    display: flex;
    margin: 5%;
    justify-content: space-around;
    flex-wrap: wrap;
    div {
      width: 45%;
      min-width: 20rem;
      background: white;
      border-radius: 1rem;
    }
    .add {
      width: 5rem;
      margin: auto;
    }
  }

  .saveButton {
    background: gray;
    color: white;
    border: 1px solid black;
    border-radius: 1rem;
    width: 5rem;
    padding: 0.5rem;
    margin: auto auto 0.5rem auto;
    cursor: pointer;
    &:hover {
      background: rgb(162, 162, 162);
    }
  }
}

.feedbackbox {
  .jsonView {
    text-align: left;
    margin: auto;
    width: 15rem;
  }
}
</style>
